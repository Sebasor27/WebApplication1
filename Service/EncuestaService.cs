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

    public async Task GuardarRespuestasEncuesta(int emprendedorId, int idEncuesta, List<EncuestaIceDto> respuestasDto)
    {
        try
        {
            var respuestas = respuestasDto.Select(dto => new EncuestasIce
            {
                IdEmprendedor = emprendedorId,
                IdEncuesta = idEncuesta,
                IdPregunta = dto.IdPregunta, 
                ValorRespuesta = dto.ValorAjustado
            }).ToList();

            await _context.EncuestasIces.AddRangeAsync(respuestas);
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
                    IdEncuesta = idEncuesta 
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

        var valorIceTotal = (double)await _context.ResultadosIces
            .Where(r => r.IdEmprendedor == emprendedorId && r.IdEncuesta == idEncuesta)
            .SumAsync(r => r.PuntuacionCompetencia);

        Console.WriteLine($"Valor ICE total calculado: {valorIceTotal}");

        var indicadores = await _context.Indicadores.ToListAsync();
        var indicadorEncontrado = indicadores.FirstOrDefault(indicador =>
        {
            try
            {
                if (string.IsNullOrWhiteSpace(indicador.Rango))
                    return false;

                var rangoLimpio = indicador.Rango.Trim();
                
                rangoLimpio = rangoLimpio.TrimStart('[', '(').TrimEnd(']', ')');
                
                var limites = rangoLimpio.Split(new[] { '-', '~', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (limites.Length != 2)
                    return false;

                if (!double.TryParse(limites[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double rangoInicio))
                    return false;

                if (!double.TryParse(limites[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double rangoFin))
                    return false;

                return valorIceTotal >= rangoInicio && valorIceTotal <= rangoFin;
            }
            catch
            {
                return false;
            }
        });

        if (indicadorEncontrado == null)
        {
            Console.WriteLine($"No se encontró indicador para el valor ICE: {valorIceTotal}");
        }

        var resumenIce = new ResumenIce
        {
            IdEmprendedor = emprendedorId,
            ValorIceTotal = (decimal)valorIceTotal,
            IdIndicadores = indicadorEncontrado?.IdIndicadores,
            IdEncuesta = idEncuesta
        };

        await _context.ResumenIces.AddAsync(resumenIce);
        await _context.SaveChangesAsync();
        
        Console.WriteLine("Resumen ICE guardado exitosamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error en CalcularIceTotal: {ex.ToString()}");
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