using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public RespuestasIepmController(CentroEmpContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<RespuestaDto>>> PostRespuestasIepm([FromBody] List<RespuestaRequest> respuestasRequest)
        {
            if (!ModelState.IsValid || respuestasRequest == null || !respuestasRequest.Any())
            {
                return BadRequest("Datos de respuesta inválidos");
            }

            var idEmprendedor = respuestasRequest.First().IdEmprendedor;
            if (respuestasRequest.Any(r => r.IdEmprendedor != idEmprendedor))
            {
                return BadRequest("Todas las respuestas deben ser para el mismo emprendedor");
            }

            var emprendedor = await _context.Emprendedores.FindAsync(idEmprendedor);
            if (emprendedor == null)
            {
                return NotFound($"Emprendedor con ID {idEmprendedor} no encontrado");
            }

            var idsPreguntas = respuestasRequest.Select(r => r.IdPregunta).Distinct().ToList();
            var preguntasExistentes = await _context.PreguntasIepms
                .Where(p => idsPreguntas.Contains(p.IdPregunta))
                .Select(p => p.IdPregunta)
                .ToListAsync();

            var preguntasNoExistentes = idsPreguntas.Except(preguntasExistentes).ToList();
            if (preguntasNoExistentes.Any())
            {
                return NotFound($"Preguntas no encontradas: {string.Join(", ", preguntasNoExistentes)}");
            }

            var nuevaEncuesta = new EncuestasIepm
            {
                IdEmprendedor = idEmprendedor,
                FechaAplicacion = DateTime.UtcNow
            };

            await _context.EncuestasIepms.AddAsync(nuevaEncuesta);
            await _context.SaveChangesAsync();

            var respuestas = respuestasRequest.Select(request => new RespuestasIepm
            {
                IdEncuesta = nuevaEncuesta.IdEncuestaIepm, 
                IdPregunta = request.IdPregunta,
                Valor = request.Valor,
                Comentarios = request.Comentarios,
                IdEmprendedor = request.IdEmprendedor
            }).ToList();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.RespuestasIepms.AddRangeAsync(respuestas);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetRespuestasByEncuesta), 
                    new { idEncuesta = nuevaEncuesta.IdEncuestaIepm, idEmprendedor }, 
                    respuestas);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al guardar las respuestas: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

   [HttpGet("encuesta/{idEncuesta}/emprendedor/{idEmprendedor}")]
public async Task<ActionResult<IEnumerable<RespuestaDto>>> GetRespuestasByEncuesta(int idEncuesta, int idEmprendedor)
{
    // 1. Verificar si existen los IDs en las tablas relacionadas
    var encuestaExists = await _context.EncuestasIepms.AnyAsync(e => e.IdEncuestaIepm == idEncuesta);
    var emprendedorExists = await _context.Emprendedores.AnyAsync(e => e.IdEmprendedor == idEmprendedor);
    
    if (!encuestaExists || !emprendedorExists)
    {
        return NotFound($"Encuesta: {encuestaExists}, Emprendedor: {emprendedorExists}");
    }

    // 2. Consulta con join para obtener la pregunta
    var respuestas = await _context.RespuestasIepms
        .Where(r => r.IdEncuesta == idEncuesta && r.IdEmprendedor == idEmprendedor)
        .Join(_context.PreguntasIepms,
            respuesta => respuesta.IdPregunta,
            pregunta => pregunta.IdPregunta,
            (respuesta, pregunta) => new
            {
                respuesta.IdRespuesta,
                respuesta.IdEncuesta,
                respuesta.IdPregunta,
                respuesta.Valor,
                respuesta.Comentarios,
                respuesta.IdEmprendedor,
                Pregunta = pregunta.Enunciado // Suponiendo que "Enunciado" es el campo que contiene la pregunta
            })
        .OrderBy(r => r.IdPregunta)
        .ToListAsync();

    if (!respuestas.Any())
    {
        return NotFound("No se encontraron respuestas para estos parámetros");
    }

    // 3. Mapear los resultados a DTO
    var respuestaDtos = respuestas.Select(r => new RespuestaDto
    {
        IdRespuesta = r.IdRespuesta,
        IdEncuesta = r.IdEncuesta,
        IdPregunta = r.IdPregunta,
        Valor = r.Valor,
        Comentarios = r.Comentarios,
        IdEmprendedor = r.IdEmprendedor,
        Pregunta = r.Pregunta
    });

    return Ok(respuestaDtos);
}


    public class RespuestaRequest
    {
        public int IdPregunta { get; set; }
        public int Valor { get; set; }
        public string? Comentarios { get; set; }
        public int IdEmprendedor { get; set; }
    }
}
}

