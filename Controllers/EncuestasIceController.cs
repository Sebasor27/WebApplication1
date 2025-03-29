using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasIceController : ControllerBase
    {
        private readonly EncuestaService _encuestaService;

        public EncuestasIceController(EncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        [HttpPost("procesar-encuesta")]
        public async Task<IActionResult> ProcesarEncuesta([FromBody] ProcesarEncuestaRequest request)
        {
            if (request == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío");
            }

            if (request.Respuestas == null || request.Respuestas.Count == 0)
            {
                return BadRequest("Debe proporcionar al menos una respuesta");
            }

            try
            {
                int idEncuesta = await _encuestaService.CrearNuevaEncuesta(request.EmprendedorId);

                await _encuestaService.GuardarRespuestasEncuesta(
                    request.EmprendedorId, 
                    idEncuesta, 
                    request.Respuestas
                );

                await _encuestaService.CalcularYGuardarPuntuacionCompetencia(request.EmprendedorId, idEncuesta);
                await _encuestaService.CalcularIceTotal(request.EmprendedorId, idEncuesta);

                return Ok(new 
                { 
                    IdEncuesta = idEncuesta, 
                    Message = "Encuesta procesada correctamente" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la encuesta: {ex.Message}");
            }
        }
    }

    

    public class ProcesarEncuestaRequest
    {
        public int EmprendedorId { get; set; }
        public List<EncuestaIceDto> Respuestas { get; set; } = new List<EncuestaIceDto>();
    }
}