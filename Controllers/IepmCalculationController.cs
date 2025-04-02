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

        [HttpGet("Encuestas/{idEmprendedor}")]
        public ActionResult<List<EncuestaConResultadoDto>> GetEncuestas(int idEmprendedor)
        {
            try
            {
                if (idEmprendedor <= 0)
                {
                    return BadRequest("ID de emprendedor inválido");
                }

                var encuestas = _iepmCalculatorService.GetEncuestasConResultados(idEmprendedor);
                return Ok(encuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener encuestas: {ex.Message}");
            }
        }

        [HttpGet("Resultado/{idEmprendedor}/{idEncuesta}")]
        public ActionResult<dynamic> GetResultado(int idEmprendedor, int idEncuesta)
        {
            try
            {
                if (idEmprendedor <= 0 || idEncuesta <= 0)
                {
                    return BadRequest("Parámetros inválidos");
                }

                var resultado = _iepmCalculatorService.GetResultadoPorEncuestaIepm(idEmprendedor, idEncuesta);

                decimal sumaDimensiones = resultado.Dimensiones.Sum(d => d.Valor);
                decimal sumaIndicadores = resultado.Indicadores.Sum(i => i.Valor);

                var response = new
                {
                    iepm = new
                    {
                        resultado.IEPM.IdResultadoIepm,
                        resultado.IEPM.IdEncuesta,
                        resultado.IEPM.IdEmprendedor,
                        resultado.IEPM.Iepm,
                        porcentaje = 100,  
                        resultado.IEPM.Valoracion,
                        resultado.IEPM.FechaCalculo,
                        resultadosAccionesIepms = resultado.IEPM.ResultadosAccionesIepms
                    },
                    dimensiones = resultado.Dimensiones.Select(d => new
                    {
                        d.IdResultadoDimension,
                        d.IdEncuesta,
                        d.IdDimension,
                        d.Valor,
                        porcentaje = sumaDimensiones == 0 ? 0 : Math.Round((double)(d.Valor / sumaDimensiones * 100), 2),
                        d.FechaCalculo
                    }),
                    indicadores = resultado.Indicadores.Select(i => new
                    {
                        i.IdResultadoIndicador,
                        i.IdEncuesta,
                        i.IdIndicador,
                        i.Valor,
                        porcentaje = sumaIndicadores == 0 ? 0 : Math.Round((double)(i.Valor / sumaIndicadores * 100), 2),
                        i.FechaCalculo
                    }),
                    accionMejora = resultado.AccionMejora
                };

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener resultado: {ex.Message}");
            }
        }

        public class IepmCalculationRequest
        {
            public int IdEmprendedor { get; set; }
            public int IdEncuesta { get; set; }
        }
    }
}