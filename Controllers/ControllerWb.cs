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

        // Inyectamos EncuestaService a través del constructor
        public EncuestaController(EncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        // Endpoint para calcular y guardar la puntuación de la competencia
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

        // Endpoint para calcular el ICE total
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
        // GET: api/EncuestasIce/encuestas/{emprendedorId}
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

        // GET: api/EncuestasIce/resultados-resumen/{emprendedorId}/{idEncuesta}
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

