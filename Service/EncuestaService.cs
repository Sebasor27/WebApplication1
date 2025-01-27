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
            // Traemos todas las competencias con sus preguntas asociadas
            var competencias = await _context.Competencias
                                             .Include(c => c.PreguntasIces)
                                             .ToListAsync();

            // Traemos todas las respuestas para el emprendedor de una vez
            var respuestas = await _context.EncuestasIces
                                           .Where(e => e.IdEmprendedor == emprendedorId)
                                           .ToListAsync();

            var tareas = competencias.Select(async competencia =>
            {
                // Filtramos las preguntas que corresponden a esta competencia
                var preguntas = competencia.PreguntasIces;

                // Filtramos las respuestas que corresponden a estas preguntas
                var respuestasCompetencia = respuestas.Where(e => preguntas.Any(p => p.IdPregunta == e.IdPregunta)).ToList();

                // Sumar las respuestas para obtener la "valoracion" de esta competencia
                var valoracion = respuestasCompetencia.Sum(r => r.ValorRespuesta);

                // Calcular la puntuación de la competencia
                var puntuacionCompetencia = CalcularPuntuacionCompetencia(valoracion, competencia.PuntosMaximos, competencia.PesoRelativo);

                // Crear el resultado
                var resultado = new ResultadosIce
                {
                    IdEmprendedor = emprendedorId,
                    IdCompetencia = competencia.IdCompetencia,
                    Valoracion = valoracion,
                    PuntuacionCompetencia = puntuacionCompetencia
                };

                // Añadir el resultado a la base de datos
                await _context.ResultadosIces.AddAsync(resultado);
            });

            // Ejecutamos todas las tareas en paralelo
            await Task.WhenAll(tareas);

            // Guardamos los cambios de una sola vez después de procesar todas las competencias
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Manejo de excepción (log, rethrow, etc.)
            throw new ApplicationException("Error al calcular y guardar la puntuación de las competencias", ex);
        }
    }

    private decimal CalcularPuntuacionCompetencia(int valoracion, int puntosMaximos, decimal pesoRelativo)
    {
        // Fórmula: (peso_relativo / puntos_maximos) * valoracion
        return (pesoRelativo / puntosMaximos) * valoracion;
    }
}
