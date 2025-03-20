using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class EncuestaService
{
    private readonly CentroEmpContext _context;

    public EncuestaService(CentroEmpContext context)
    {
        _context = context;
    }

    public async Task CalcularYGuardarPuntuacionCompetencia(int emprendedorId)
    {
        try
        {
            var competencias = await _context.Competencias
                                             .Include(c => c.PreguntasIces)
                                             .ToListAsync();

            var respuestas = await _context.EncuestasIces
                                           .Where(e => e.IdEmprendedor == emprendedorId)
                                           .ToListAsync();

            var tareas = competencias.Select(async competencia =>
            {
                var preguntas = competencia.PreguntasIces;

                var respuestasCompetencia = respuestas.Where(e => preguntas.Any(p => p.IdPregunta == e.IdPregunta)).ToList();

                var valoracion = respuestasCompetencia.Sum(r => r.ValorRespuesta);

                var puntuacionCompetencia = CalcularPuntuacionCompetencia(valoracion, competencia.PuntosMaximos, competencia.PesoRelativo);

                var resultado = new ResultadosIce
                {
                    IdEmprendedor = emprendedorId,
                    IdCompetencia = competencia.IdCompetencia,
                    Valoracion = valoracion,
                    PuntuacionCompetencia = puntuacionCompetencia
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

    public async Task CalcularIceTotal(int emprendedorId)
{
    try
    {
        // Obtener los resultados de las competencias del emprendedor
        var resultados = await _context.ResultadosIces
                                            .Where(r => r.IdEmprendedor == emprendedorId)
                                            .ToListAsync();

        // Calcular el ICE total sumando las puntuaciones de las competencias
        double valorIceTotal = (double)resultados.Sum(r => r.PuntuacionCompetencia);

        // Obtener todos los indicadores de la base de datos
        var indicadores = await _context.Indicadores.ToListAsync();

        // Buscar el indicador cuyo rango contiene el valorIceTotal
        var indicadorEncontrado = indicadores.FirstOrDefault(indicador => 
        {
            if (string.IsNullOrEmpty(indicador.Rango))
                return false;

            // Limpiar el rango y dividirlo en límites
            var rango = indicador.Rango.Trim('[', ']', '(', ')');
            var limites = rango.Split('-');

            if (limites.Length != 2)
                return false;

            // Convertir los límites a double
            double rangoInicio = double.Parse(limites[0].Trim(), CultureInfo.InvariantCulture);
            double rangoFin = double.Parse(limites[1].Trim(), CultureInfo.InvariantCulture);

            // Verificar si el valorIceTotal está dentro del rango
            return valorIceTotal >= rangoInicio && valorIceTotal < rangoFin;
        });

        // Crear el resumen ICE
        var resumenIce = new ResumenIce
        {
            IdEmprendedor = emprendedorId,
            ValorIceTotal = (decimal)valorIceTotal,
            IdIndicadores = indicadorEncontrado?.IdIndicadores // Asignar el id_indicadores si se encuentra
        };

        // Guardar el resumen ICE en la base de datos
        await _context.ResumenIces.AddAsync(resumenIce);
        await _context.SaveChangesAsync();

        // Mostrar el resultado en consola
        if (indicadorEncontrado != null)
        {
            Console.WriteLine($"Indicador encontrado: {indicadorEncontrado.Valoracion} (ID: {indicadorEncontrado.IdIndicadores})");
        }
        else
        {
            Console.WriteLine("Indicador no encontrado para el ICE total calculado.");
        }
    }
    catch (Exception ex)
    {
        throw new ApplicationException("Error al calcular el ICE total", ex);
    }
}
    private decimal CalcularPuntuacionCompetencia(int valoracion, int puntosMaximos, decimal pesoRelativo)
    {
        // Fórmula: (peso_relativo / puntos_maximos) * valoracion
        return (pesoRelativo / puntosMaximos) * valoracion;
    }
}
