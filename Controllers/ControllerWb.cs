using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncuestaController : ControllerBase
    {
        private readonly EncuestaService _encuestaService;

        public EncuestaController(EncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        [HttpPost("calcular-puntuacion")]
        public async Task<IActionResult> CalcularPuntuacionCompetencia(int emprendedorId, int idEncuesta)
        {
            try
            {
                await _encuestaService.CalcularYGuardarPuntuacionCompetencia(emprendedorId, idEncuesta);
                return Ok(new { message = "Puntuación calculada y guardada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular la puntuación: {ex.Message}");
            }
        }

        [HttpPost("calcular-ice-total")]
        public async Task<IActionResult> CalcularIceTotal(int emprendedorId, int idEncuesta)
        {
            try
            {
                await _encuestaService.CalcularIceTotal(emprendedorId, idEncuesta);
                return Ok(new { message = "ICE total calculado y guardado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular el ICE total: {ex.Message}");
            }
        }
        [HttpGet("encuestas/{emprendedorId}")]
        public async Task<IActionResult> ObtenerEncuestas(int emprendedorId)
        {
            try
            {
                var encuestas = await _encuestaService.ObtenerEncuestasDeEmprendedor(emprendedorId);
                return Ok(encuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las encuestas: {ex.Message}");
            }
        }

        [HttpGet("resultados-resumen/{emprendedorId}/{idEncuesta}")]
        public async Task<IActionResult> ObtenerResultadosYResumen(int emprendedorId, int idEncuesta)
        {
            try
            {
                var resultadosYResumen = await _encuestaService.ObtenerResultadosYResumenDeEncuesta(emprendedorId, idEncuesta);
                return Ok(resultadosYResumen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los resultados y el resumen: {ex.Message}");
            }
        }
    }
}

