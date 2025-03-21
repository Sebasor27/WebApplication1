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

        public EncuestasIceController(EncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        [HttpPost("procesar-encuesta")]
        public async Task<IActionResult> ProcesarEncuesta(int emprendedorId, List<EncuestasIce> respuestas)
        {
            try
            {
                int idEncuesta = await _encuestaService.CrearNuevaEncuesta(emprendedorId);

                await _encuestaService.GuardarRespuestasEncuesta(emprendedorId, idEncuesta, respuestas);

                await _encuestaService.CalcularYGuardarPuntuacionCompetencia(emprendedorId, idEncuesta);

                await _encuestaService.CalcularIceTotal(emprendedorId, idEncuesta);

                return Ok(new 
                { 
                    idEncuesta = idEncuesta, 
                    message = "Encuesta procesada correctamente, respuestas guardadas, puntuación calculada y ICE total calculado." 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la encuesta: {ex.Message}");
            }
        }
    }
}