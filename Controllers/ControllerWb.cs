using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CalcularPuntuacionCompetencia(int emprendedorId)
        {
            // Llamar al servicio para calcular la puntuación
            await _encuestaService.CalcularYGuardarPuntuacionCompetencia(emprendedorId);

            // Retornar una respuesta de éxito
            return Ok(new { message = "Puntuación calculada y guardada correctamente" });
        }
        [HttpPost("calcular-IceTotal")]
        public async Task<IActionResult> calcularIceTotal(int emprendedorId)
        {
            // Llamar al servicio para calcular la puntuación
            await _encuestaService.CalcularIceTotal(emprendedorId);

            // Retornar una respuesta de éxito
            return Ok(new { message = "Puntuación calculada y guardada correctamente" });
        }
    }
}
