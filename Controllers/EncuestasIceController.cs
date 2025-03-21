using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasIceController : ControllerBase
    {
        private readonly EncuestaService _encuestaService;

        // Inyectamos EncuestaService a través del constructor
        public EncuestasIceController(EncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        // POST: api/EncuestasIce/crear-encuesta
        [HttpPost("crear-encuesta")]
        public async Task<IActionResult> CrearEncuesta(int emprendedorId)
        {
            try
            {
                int idEncuesta = await _encuestaService.CrearNuevaEncuesta(emprendedorId);
                return Ok(new { idEncuesta = idEncuesta, message = "Encuesta creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la encuesta: {ex.Message}");
            }
        }

        // POST: api/EncuestasIce/guardar-respuestas
        [HttpPost("guardar-respuestas")]
        public async Task<IActionResult> GuardarRespuestas(int emprendedorId, int idEncuesta, List<EncuestasIce> respuestas)
        {
            try
            {
                await _encuestaService.GuardarRespuestasEncuesta(emprendedorId, idEncuesta, respuestas);
                return Ok(new { message = "Respuestas guardadas correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar las respuestas: {ex.Message}");
            }
        }
    }
}