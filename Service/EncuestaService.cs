using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class EncuestaService
{
    private readonly CentroEmpContext _context;

    public EncuestaService(CentroEmpContext context)
    {
        _context = context;
    }

    public async Task<int> CrearNuevaEncuesta(int emprendedorId)
    {

        var ultimaEncuesta = await _context.EncuestasIces
                                          .Where(e => e.IdEmprendedor == emprendedorId)
                                          .OrderByDescending(e => e.IdEncuesta)
                                          .FirstOrDefaultAsync();

        int nuevoIdEncuesta = ultimaEncuesta == null ? 1 : ultimaEncuesta.IdEncuesta + 1;

        return nuevoIdEncuesta;
    }


    public async Task GuardarRespuestasEncuesta(int emprendedorId, int idEncuesta, List<EncuestasIce> respuestas)
    {
        try
        {
            foreach (var respuesta in respuestas)
            {

                respuesta.IdEmprendedor = emprendedorId;
                respuesta.IdEncuesta = idEncuesta;


                await _context.EncuestasIces.AddAsync(respuesta);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al guardar las respuestas de la encuesta", ex);
        }
    }

    public async Task CalcularYGuardarPuntuacionCompetencia(int emprendedorId, int idEncuesta)
    {
        try
        {

            var existeCalculo = await _context.ResultadosIces
                                              .AnyAsync(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta);

            if (existeCalculo)
            {
                throw new ApplicationException("Ya existe un cálculo de puntuación para esta encuesta y emprendedor.");
            }


            var competencias = await _context.Competencias
                                             .Include(c => c.PreguntasIces)
                                             .ToListAsync();

            var respuestas = await _context.EncuestasIces
                                           .Where(e => e.IdEmprendedor == emprendedorId && e.IdEncuesta == idEncuesta)
                                           .ToListAsync();

            var tareas = competencias.Select(async competencia =>
            {
                var preguntas = competencia.PreguntasIces;


                var respuestasCompetencia = respuestas
                    .Where(e => preguntas.Any(p => p.IdPregunta == e.IdPregunta))
                    .ToList();

                var valoracion = respuestasCompetencia.Sum(r => r.ValorRespuesta);


                var puntuacionCompetencia = CalcularPuntuacionCompetencia(valoracion, competencia.PuntosMaximos, competencia.PesoRelativo);

                var resultado = new ResultadosIce
                {
                    IdEmprendedor = emprendedorId,
                    IdCompetencia = competencia.IdCompetencia,
                    Valoracion = valoracion,
                    PuntuacionCompetencia = puntuacionCompetencia,
                    IdEncuesta = idEncuesta // Vincular el resultado a la encuesta específica
                };

                await _context.ResultadosIces.AddAsync(resultado);
            });

            await Task.WhenAll(tareas);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al calcular y guardar la puntuación de las competencias", ex);
        }
    }

    public async Task CalcularIceTotal(int emprendedorId, int idEncuesta)
    {
        try
        {
            var resumenExistente = await _context.ResumenIces
                                                 .FirstOrDefaultAsync(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta);

            if (resumenExistente != null)
            {
                throw new ApplicationException("Ya existe un resumen ICE para esta encuesta.");
            }

            var resultados = await _context.ResultadosIces
                                                .Where(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta)
                                                .ToListAsync();

            double valorIceTotal = (double)resultados.Sum(r => r.PuntuacionCompetencia);

            var indicadores = await _context.Indicadores.ToListAsync();

            var indicadorEncontrado = indicadores.FirstOrDefault(indicador =>
            {
                if (string.IsNullOrEmpty(indicador.Rango))
                    return false;

                var rango = indicador.Rango.Trim('[', ']', '(', ')');
                var limites = rango.Split('-');

                if (limites.Length != 2)
                    return false;

                double rangoInicio = double.Parse(limites[0].Trim(), CultureInfo.InvariantCulture);
                double rangoFin = double.Parse(limites[1].Trim(), CultureInfo.InvariantCulture);

                return valorIceTotal >= rangoInicio && valorIceTotal < rangoFin;
            });

            var resumenIce = new ResumenIce
            {
                IdEmprendedor = emprendedorId,
                ValorIceTotal = (decimal)valorIceTotal,
                IdIndicadores = indicadorEncontrado?.IdIndicadores, 
                IdEncuesta = idEncuesta 
            };

            await _context.ResumenIces.AddAsync(resumenIce);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al calcular el ICE total", ex);
        }
    }

    public async Task<List<int>> ObtenerEncuestasDeEmprendedor(int emprendedorId)
    {
        return await _context.EncuestasIces
                            .Where(e => e.IdEmprendedor == emprendedorId)
                            .Select(e => e.IdEncuesta)
                            .Distinct()
                            .ToListAsync();
    }

    public async Task<object> ObtenerResultadosYResumenDeEncuesta(int emprendedorId, int idEncuesta)
    {

        var resultados = await _context.ResultadosIces
                                      .Where(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta)
                                      .ToListAsync();


        var resumen = await _context.ResumenIces
                                    .FirstOrDefaultAsync(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta);


        return new
        {
            Resultados = resultados,
            Resumen = resumen
        };
    }

    private decimal CalcularPuntuacionCompetencia(int valoracion, int puntosMaximos, decimal pesoRelativo)
    {
        return (pesoRelativo / puntosMaximos) * valoracion;
    }
}