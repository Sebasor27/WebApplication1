using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IepmCalculationController : ControllerBase
    {
        private readonly IIepmCalculatorService _iepmCalculatorService;

        public IepmCalculationController(IIepmCalculatorService iepmCalculatorService)
        {
            _iepmCalculatorService = iepmCalculatorService;
        }

        [HttpPost("CalculateIEPM")]
        public ActionResult<ResultadoIepmCompleto> CalculateIEPM([FromBody] IepmCalculationRequest request)
        {
            try
            {
                if (request == null || request.IdEmprendedor <= 0 || request.IdEncuesta <= 0)
                {
                    return BadRequest("Invalid request data");
                }

                var result = _iepmCalculatorService.CalculateAndSaveIEPM(request.IdEmprendedor, request.IdEncuesta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("LastResult/{idEmprendedor}")]
        public ActionResult<ResultadoIepmCompleto> GetLastResult(int idEmprendedor)
        {
            try
            {
                if (idEmprendedor <= 0)
                {
                    return BadRequest("Invalid emprendedor ID");
                }

                var result = _iepmCalculatorService.GetLastResult(idEmprendedor);
                
                if (result == null)
                {
                    return NotFound("No results found for this emprendedor");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
    }

    public class IepmCalculationRequest
    {
        public int IdEmprendedor { get; set; }
        public int IdEncuesta { get; set; }
    }
}