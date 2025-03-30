using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;
        private readonly IIepmCalculatorService _iepmCalculatorService;

        public RespuestasIepmController(CentroEmpContext context, IIepmCalculatorService iepmCalculatorService)
        {
            _context = context;
            _iepmCalculatorService = iepmCalculatorService;
        }

        [HttpPost]
        public async Task<ActionResult> PostRespuestasIepm([FromBody] List<RespuestaRequest> respuestasRequest)
        {
            // Validaciones existentes (mantenemos todo tu código actual)
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

            // Crear nueva encuesta
            var nuevaEncuesta = new EncuestasIepm
            {
                IdEmprendedor = idEmprendedor,
                FechaAplicacion = DateTime.UtcNow
            };

            await _context.EncuestasIepms.AddAsync(nuevaEncuesta);
            await _context.SaveChangesAsync();

            // Mapear respuestas
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

                var resultadoCompleto = _iepmCalculatorService.CalculateAndSaveIEPM(
                    idEmprendedor,
                    nuevaEncuesta.IdEncuestaIepm);

                await transaction.CommitAsync();

                return Ok(new
                {
                    Success = true,
                    Encuesta = new
                    {
                        Id = nuevaEncuesta.IdEncuestaIepm,
                        Fecha = nuevaEncuesta.FechaAplicacion
                    },
                    Resultado = new
                    {
                        Id = resultadoCompleto.IEPM.IdResultadoIepm,
                        Puntaje = resultadoCompleto.IEPM.Iepm,
                        Valoracion = resultadoCompleto.IEPM.Valoracion,
                        FechaCalculo = resultadoCompleto.IEPM.FechaCalculo
                    },
                    Dimensiones = resultadoCompleto.Dimensiones.Select(d => new
                    {
                        Id = d.IdDimension,
                        Puntaje = d.Valor
                    }),
                    AccionMejora = resultadoCompleto.AccionMejora != null ? new
                    {
                        Id = resultadoCompleto.AccionMejora.IdAccion,
                        Descripcion = resultadoCompleto.AccionMejora.Descripcion
                    } : null
                });
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    Success = false,
                    Error = "Error de base de datos",
                    Details = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                await transaction.RollbackAsync();
                return NotFound(new
                {
                    Success = false,
                    Error = "Recurso no encontrado",
                    Details = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new
                {
                    Success = false,
                    Error = "Operación inválida",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                // Verificar si al menos se guardó la encuesta
                if (await _context.EncuestasIepms.AnyAsync(e => e.IdEncuestaIepm == nuevaEncuesta.IdEncuestaIepm))
                {
                    return Ok(new
                    {
                        Success = true,
                        Warning = "Encuesta guardada pero cálculo no completado",
                        EncuestaId = nuevaEncuesta.IdEncuestaIepm,
                        Error = ex.Message
                    });
                }

                return StatusCode(500, new
                {
                    Success = false,
                    Error = "Error inesperado",
                    Details = ex.Message
                });
            }
        }
        [HttpGet("encuesta/{idEncuesta}/emprendedor/{idEmprendedor}")]
        public async Task<ActionResult<IEnumerable<RespuestaDto>>> GetRespuestasByEncuesta(int idEncuesta, int idEmprendedor)
        {
            var encuestaExists = await _context.EncuestasIepms.AnyAsync(e => e.IdEncuestaIepm == idEncuesta);
            var emprendedorExists = await _context.Emprendedores.AnyAsync(e => e.IdEmprendedor == idEmprendedor);

            if (!encuestaExists || !emprendedorExists)
            {
                return NotFound($"Encuesta: {encuestaExists}, Emprendedor: {emprendedorExists}");
            }

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
                        Pregunta = pregunta.Enunciado
                    })
                .OrderBy(r => r.IdPregunta)
                .ToListAsync();

            if (!respuestas.Any())
            {
                return NotFound("No se encontraron respuestas para estos parámetros");
            }

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

