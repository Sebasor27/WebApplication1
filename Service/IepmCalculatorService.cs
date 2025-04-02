using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IIepmCalculatorService
    {
        ResultadoIepmCompleto CalculateAndSaveIEPM(int idEmprendedor, int idEncuesta);
        ResultadoIepmCompleto GetLastResult(int idEmprendedor);

        List<EncuestaConResultadoDto> GetEncuestasConResultados(int idEmprendedor); 
        ResultadoIepmCompleto GetResultadoPorEncuestaIepm(int idEmprendedor, int idEncuesta); 
    }

    public class IepmCalculatorService : IIepmCalculatorService
    {
        private readonly CentroEmpContext _context;

        public IepmCalculatorService(CentroEmpContext context)
        {
            _context = context;
        }

        public ResultadoIepmCompleto CalculateAndSaveIEPM(int idEmprendedor, int idEncuesta)
        {
            var emprendedor = _context.Emprendedores
                .FirstOrDefault(e => e.IdEmprendedor == idEmprendedor);

            if (emprendedor == null)
                throw new KeyNotFoundException($"No se encontró el emprendedor con ID {idEmprendedor}");

            var encuesta = _context.EncuestasIepms
                .Include(e => e.RespuestasIepms)
                .FirstOrDefault(e => e.IdEncuestaIepm == idEncuesta);

            if (encuesta == null)
                throw new KeyNotFoundException($"No se encontró la encuesta con ID {idEncuesta}");

            if (!encuesta.RespuestasIepms.Any())
                throw new InvalidOperationException("La encuesta no tiene respuestas asociadas");

            try
            {
                var indicatorResults = CalculateIndicatorResults(idEmprendedor, idEncuesta);
                var dimensionResults = CalculateDimensionResults(indicatorResults);
                var iepmScore = CalculateIEPM(dimensionResults);
                var valoracion = GetEvaluation(iepmScore);
                var accionMejora = GetAccionMejora(iepmScore);

                ValidateResultsBeforeSave(dimensionResults, indicatorResults, accionMejora);

                var resultadoIepm = SaveResults(
                    idEmprendedor,
                    idEncuesta,
                    iepmScore,
                    valoracion,
                    dimensionResults,
                    indicatorResults,
                    accionMejora
                );

                return new ResultadoIepmCompleto
                {
                    IEPM = resultadoIepm,
                    Dimensiones = dimensionResults,
                    Indicadores = indicatorResults,
                    AccionMejora = accionMejora
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error completo: {ex.ToString()}");
                throw;
            }
        }
        public ResultadoIepmCompleto GetLastResult(int idEmprendedor)
        {
            var resultadoIepm = _context.ResultadosIepms
                .AsNoTracking()
                .Include(r => r.ResultadosAccionesIepms)
                    .ThenInclude(ra => ra.IdAccionNavigation)
                .Where(r => r.IdEmprendedor == idEmprendedor)
                .OrderByDescending(r => r.FechaCalculo)
                .FirstOrDefault();

            if (resultadoIepm == null)
                return null;

            var resultadosCompletos = new ResultadoIepmCompleto
            {
                IEPM = resultadoIepm,
                Dimensiones = _context.ResultadosDimensionesIepms
                    .AsNoTracking()
                    .Where(d => d.IdEncuesta == resultadoIepm.IdEncuesta)
                    .ToList(),
                Indicadores = _context.ResultadosIndicadoresIepms
                    .AsNoTracking()
                    .Where(i => i.IdEncuesta == resultadoIepm.IdEncuesta)
                    .ToList(),
                AccionMejora = resultadoIepm.ResultadosAccionesIepms.FirstOrDefault()?.IdAccionNavigation
            };

            return resultadosCompletos;
        }

        private List<ResultadosIndicadoresIepm> CalculateIndicatorResults(int idEmprendedor, int idEncuesta)
        {
            var respuestas = _context.RespuestasIepms
                .Where(r => r.IdEmprendedor == idEmprendedor && r.IdEncuesta == idEncuesta)
                .ToList();

            var resultados = respuestas
                .Join(_context.PreguntasIepms,
                    r => r.IdPregunta,
                    p => p.IdPregunta,
                    (r, p) => new { Respuesta = r, Pregunta = p })
                .Join(_context.IndicadoresIepms,
                    rp => rp.Pregunta.IdIndicador,
                    i => i.IdIndicador,
                    (rp, i) => new { rp.Respuesta, rp.Pregunta, Indicador = i })
                .GroupBy(x => x.Indicador.IdIndicador)
                .Select(g => new ResultadosIndicadoresIepm
                {
                    IdEncuesta = idEncuesta,
                    IdIndicador = g.Key,
                    Valor = (decimal)g.Average(x => x.Respuesta.Valor),
                    FechaCalculo = DateTime.Now
                })
                .ToList();

            return resultados;
        }

        private List<ResultadosDimensionesIepm> CalculateDimensionResults(List<ResultadosIndicadoresIepm> indicatorResults)
        {
            var dimensionResults = indicatorResults
                .Join(_context.IndicadoresIepms,
                    ri => ri.IdIndicador,
                    i => i.IdIndicador,
                    (ri, i) => new { ResultadoIndicador = ri, Indicador = i })
                .Join(_context.DimensionesIepms,
                    rii => rii.Indicador.IdDimension,
                    d => d.IdDimensionIepm,
                    (rii, d) => new { rii.ResultadoIndicador, rii.Indicador, Dimension = d })
                .GroupBy(x => x.Dimension.IdDimensionIepm)
                .Select(g => new ResultadosDimensionesIepm
                {
                    IdEncuesta = g.First().ResultadoIndicador.IdEncuesta,
                    IdDimension = g.Key,
                    Valor = g.Sum(x => x.ResultadoIndicador.Valor * x.Indicador.Peso) /
                            g.Sum(x => x.Indicador.Peso),
                    FechaCalculo = DateTime.Now
                })
                .ToList();

            return dimensionResults;
        }

        private void ValidateResultsBeforeSave(
    List<ResultadosDimensionesIepm> dimensionResults,
    List<ResultadosIndicadoresIepm> indicatorResults,
    AccionesMejoraIepm accionMejora)
        {
            var dimensionesIds = dimensionResults.Select(d => d.IdDimension).Distinct().ToList();
            var dimensionesExistentes = _context.DimensionesIepms
                .Where(d => dimensionesIds.Contains(d.IdDimensionIepm))
                .Select(d => d.IdDimensionIepm)
                .ToList();

            var dimensionesFaltantes = dimensionesIds.Except(dimensionesExistentes).ToList();
            if (dimensionesFaltantes.Any())
                throw new KeyNotFoundException($"Dimensiones no encontradas: {string.Join(", ", dimensionesFaltantes)}");

            var indicadoresIds = indicatorResults.Select(i => i.IdIndicador).Distinct().ToList();
            var indicadoresExistentes = _context.IndicadoresIepms
                .Where(i => indicadoresIds.Contains(i.IdIndicador))
                .Select(i => i.IdIndicador)
                .ToList();

            var indicadoresFaltantes = indicadoresIds.Except(indicadoresExistentes).ToList();
            if (indicadoresFaltantes.Any())
                throw new KeyNotFoundException($"Indicadores no encontrados: {string.Join(", ", indicadoresFaltantes)}");
            if (accionMejora != null && !_context.AccionesMejoraIepms.Any(a => a.IdAccion == accionMejora.IdAccion))
                throw new KeyNotFoundException($"Acción de mejora no encontrada: ID {accionMejora.IdAccion}");
        }

        private decimal CalculateIEPM(List<ResultadosDimensionesIepm> dimensionResults)
        {
            var dimensionesConPeso = dimensionResults
                .Join(_context.DimensionesIepms,
                    rd => rd.IdDimension,
                    d => d.IdDimensionIepm,
                    (rd, d) => new { Resultado = rd, Peso = d.Peso });

            return dimensionesConPeso.Sum(x => x.Resultado.Valor * x.Peso) / 5m;
        }

        public List<EncuestaConResultadoDto> GetEncuestasConResultados(int idEmprendedor)
        {
            return _context.EncuestasIepms
                .Where(e => e.IdEmprendedor == idEmprendedor)
                .Select(e => new EncuestaConResultadoDto
                {
                    IdEncuesta = e.IdEncuestaIepm,
                    FechaAplicacion = (DateTime)e.FechaAplicacion,
                    CantidadRespuestas = e.RespuestasIepms.Count,
                    IepmTotal = _context.ResultadosIepms
                        .Where(r => r.IdEncuesta == e.IdEncuestaIepm)
                        .Select(r => r.Iepm)
                        .FirstOrDefault(),
                    Valoracion = _context.ResultadosIepms
                        .Where(r => r.IdEncuesta == e.IdEncuestaIepm)
                        .Select(r => r.Valoracion)
                        .FirstOrDefault()
                })
                .OrderByDescending(e => e.FechaAplicacion)
                .ToList();
        }

        public ResultadoIepmCompleto GetResultadoPorEncuestaIepm(int idEmprendedor, int idEncuesta)
        {
            if (!_context.EncuestasIepms.Any(e => e.IdEncuestaIepm == idEncuesta && e.IdEmprendedor == idEmprendedor))
            {
                throw new KeyNotFoundException("La encuesta no pertenece al emprendedor especificado");
            }

            var resultado = _context.ResultadosIepms
                .AsNoTracking()
                .Include(r => r.ResultadosAccionesIepms)
                    .ThenInclude(ra => ra.IdAccionNavigation)
                .FirstOrDefault(r => r.IdEncuesta == idEncuesta);

            if (resultado == null)
            {
                throw new KeyNotFoundException("No se encontraron resultados para esta encuesta");
            }

            return new ResultadoIepmCompleto
            {
                IEPM = resultado,
                Dimensiones = _context.ResultadosDimensionesIepms
                    .AsNoTracking()
                    .Where(d => d.IdEncuesta == idEncuesta)
                    .ToList(),
                Indicadores = _context.ResultadosIndicadoresIepms
                    .AsNoTracking()
                    .Where(i => i.IdEncuesta == idEncuesta)
                    .ToList(),
                AccionMejora = resultado.ResultadosAccionesIepms.FirstOrDefault()?.IdAccionNavigation
            };
        }

        private string GetEvaluation(decimal iepmScore)
        {
            if (iepmScore < 0.3m) return "Muy inadecuada puesta en marcha";
            if (iepmScore < 0.6m) return "Inadecuada puesta en marcha";
            if (iepmScore < 0.8m) return "Adecuada puesta en marcha";
            return "Muy adecuada puesta en marcha";
        }

        private AccionesMejoraIepm GetAccionMejora(decimal iepmScore)
        {
            return _context.AccionesMejoraIepms
                .FirstOrDefault(a => iepmScore >= a.RangoMin && iepmScore < a.RangoMax);
        }

        private ResultadosIepm SaveResults(
      int idEmprendedor,
      int idEncuesta,
      decimal iepmScore,
      string valoracion,
      List<ResultadosDimensionesIepm> dimensionResults,
      List<ResultadosIndicadoresIepm> indicatorResults,
      AccionesMejoraIepm accionMejora)
        {
            try
            {
                var resultadoIepm = new ResultadosIepm
                {
                    IdEncuesta = idEncuesta,
                    IdEmprendedor = idEmprendedor,
                    Iepm = iepmScore,
                    Valoracion = valoracion,
                    FechaCalculo = DateTime.Now
                };

                _context.ResultadosIepms.Add(resultadoIepm);
                _context.SaveChanges();

                foreach (var dim in dimensionResults)
                {
                    dim.IdEncuesta = idEncuesta;
                    _context.ResultadosDimensionesIepms.Add(dim);
                }

                foreach (var ind in indicatorResults)
                {
                    ind.IdEncuesta = idEncuesta;
                    _context.ResultadosIndicadoresIepms.Add(ind);
                }

                if (accionMejora != null)
                {
                    _context.ResultadosAccionesIepms.Add(new ResultadosAccionesIepm
                    {
                        IdResultadoIepm = resultadoIepm.IdResultadoIepm,
                        IdAccion = accionMejora.IdAccion
                    });
                }

                _context.SaveChanges();
                return resultadoIepm;
            }
            catch (DbUpdateException dbEx)
            {
                var errorMessages = new List<string>();
                var innerEx = dbEx.InnerException;

                while (innerEx != null)
                {
                    errorMessages.Add(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }

                throw new Exception($"Error de base de datos: {string.Join(" >> ", errorMessages)}", dbEx);
            }
        }


        public ResultadoIepmCompleto GetResultadoPorEncuesta(int idEmprendedor, int idEncuesta)
        {
            throw new NotImplementedException();
        }
    }

    public class ResultadoIepmCompleto
    {
        public ResultadosIepm IEPM { get; set; }
        public List<ResultadosDimensionesIepm> Dimensiones { get; set; }
        public List<ResultadosIndicadoresIepm> Indicadores { get; set; }
        public AccionesMejoraIepm AccionMejora { get; set; }
    }
}