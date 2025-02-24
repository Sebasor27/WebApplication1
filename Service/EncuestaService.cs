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

    public async Task calcularIceTotal(int emprendedorId)
    {
        try
        {
            
            // Obtener los resultados de las competencias del emprendedor
            var resultados = await _context.ResultadosIces
                                                .Where(r => r.IdEmprendedor == emprendedorId)
                                                .ToListAsync();
    
                var ValoriceTotal = resultados.Sum(r => r.PuntuacionCompetencia);
    
                var emprendedor = await _context.Emprendedores.FindAsync(emprendedorId);
    
                var nuevoResumenIce = new ResumenIce
                {
                    IdEmprendedor = emprendedorId,
                    ValorIceTotal = ValoriceTotal
                };

                await _context.ResumenIces.AddAsync(nuevoResumenIce);
    
                await _context.SaveChangesAsync();
            // calcular indicador en rango de valorice total
            var indicadores = _context.Indicadores.ToList();
            var indicadorEncontrado = indicadores.FirstOrDefault(indicador => {
                var rango = indicador.Rango?.Trim('[',')') ?? string.Empty;
                var limite = (indicador.Rango ?? string.Empty).Split("-");

                double rangoInicio = double.Parse(limite[0].Trim(), CultureInfo.InvariantCulture);
                double rangoFin = double.Parse(limite[1].Trim(), CultureInfo.InvariantCulture);

                return (double)ValoriceTotal >= rangoInicio && (double)ValoriceTotal < rangoFin;
            });
            if (indicadorEncontrado != null)
            {
                var resumenIce = new ResumenIce
                {
                    IdEmprendedor = emprendedorId,
                    ValorIceTotal = ValoriceTotal,
                    IdIndicadores = indicadorEncontrado.IdIndicadores
                };
                await _context.ResumenIces.AddAsync(resumenIce);
                await _context.SaveChangesAsync();

                Console.WriteLine("Indicador encontrado: ", indicadorEncontrado);
            }else {
                Console.WriteLine("Indicador no encontrado");
            }
            


        }
        catch(Exception ex)
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
