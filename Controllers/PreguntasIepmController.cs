using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public PreguntasIepmController(CentroEmpContext context)
        {
            _context = context;
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<object>>> GetDetailedPreguntasIepms()
        {
            var preguntas = await _context.PreguntasIepms
                .Include(p => p.IdIndicadorNavigation)
                    .ThenInclude(i => i.CriteriosEvaluacionIepms)
                .Include(p => p.IdCuestionarioNavigation)
                .Select(p => new
                {
                    p.IdPregunta,
                    p.IdCuestionario,
                    Destinatario = p.IdCuestionarioNavigation.Destinatario,
                    Indicador = p.IdIndicadorNavigation.Nombre,
                    p.Enunciado,
                    p.Orden,
                    CriteriosEvaluacion = p.IdIndicadorNavigation.CriteriosEvaluacionIepms
                        .Select(c => new
                        {
                            c.IdCriterio,
                            c.Descripcion,
                            c.Valor
                        })
                })
                .OrderBy(p => p.IdCuestionario)
                .ThenBy(p => p.Orden)
                .ToListAsync();

            return Ok(preguntas);
        }

        [HttpGet("ResultadosCompletos/{idEmprendedor}")]
        public ActionResult<object> GetResultadosCompletos(int idEmprendedor)
        {
            try
            {
                var emprendedor = _context.Emprendedores
                    .Where(e => e.IdEmprendedor == idEmprendedor)
                    .Select(e => new
                    {
                        e.Nombre,
                        e.Cedula
                    })
                    .FirstOrDefault();

                if (emprendedor == null)
                    return NotFound($"No se encontró el emprendedor con ID {idEmprendedor}");

                var resultadoIEPM = _context.ResultadosIepms
                    .Where(r => r.IdEmprendedor == idEmprendedor)
                    .OrderByDescending(r => r.FechaCalculo)
                    .FirstOrDefault();

                if (resultadoIEPM == null)
                    return NotFound("No se encontraron resultados para este emprendedor");

                var resultadosDimensiones = _context.ResultadosDimensionesIepms
                    .Where(rd => rd.IdEncuesta == resultadoIEPM.IdEncuesta)
                    .Join(_context.DimensionesIepms,
                        rd => rd.IdDimension,
                        d => d.IdDimensionIepm,
                        (rd, d) => new
                        {
                            Dimension = d.Nombre,
                            Puntaje = rd.Valor,
                            Peso = d.Peso 
                        })
                    .ToList();

                decimal iepmNormalizado = resultadosDimensiones.Sum(rd => rd.Puntaje * rd.Peso) / 5m;

                string valoracion;
                if (iepmNormalizado < 0.3m)
                    valoracion = "Muy inadecuada puesta en marcha";
                else if (iepmNormalizado < 0.6m)
                    valoracion = "Inadecuada puesta en marcha";
                else if (iepmNormalizado < 0.8m)
                    valoracion = "Adecuada puesta en marcha";
                else
                    valoracion = "Muy adecuada puesta en marcha";

                bool esExitoso = iepmNormalizado >= 0.6m;

                var resultadosIndicadores = _context.ResultadosIndicadoresIepms
                    .Where(ri => ri.IdEncuesta == resultadoIEPM.IdEncuesta)
                    .Join(_context.IndicadoresIepms,
                        ri => ri.IdIndicador,
                        i => i.IdIndicador,
                        (ri, i) => new
                        {
                            Indicador = i.Nombre,
                            Puntaje = ri.Valor,
                            Porcentaje = Math.Round(ri.Valor * 100, 2),
                            i.IdDimension,
                            Dimension = _context.DimensionesIepms
                                .Where(d => d.IdDimensionIepm == i.IdDimension)
                                .Select(d => d.Nombre)
                                .FirstOrDefault(),
                            Peso = i.Peso 
                        })
                    .ToList();

                var accionMejora = _context.ResultadosAccionesIepms
                    .Where(ra => ra.IdResultadoIepm == resultadoIEPM.IdResultadoIepm)
                    .Select(ra => ra.IdAccionNavigation)
                    .FirstOrDefault();

                var response = new
                {
                    Emprendedor = $"{emprendedor.Nombre} {emprendedor.Cedula}",
                    ResultadoTotal = new
                    {
                        Puntaje = Math.Round(iepmNormalizado, 4),
                        Porcentaje = Math.Round(iepmNormalizado * 100, 2),
                        Valoracion = valoracion,
                        EsExitoso = esExitoso,
                        Criterio = esExitoso ? "Adecuada/Muy adecuada puesta en marcha"
                                            : "Inadecuada/Muy inadecuada puesta en marcha"
                    },
                    PorDimension = resultadosDimensiones.Select(rd => new
                    {
                        rd.Dimension,
                        Puntaje = Math.Round(rd.Puntaje, 4),
                        Porcentaje = Math.Round(rd.Puntaje * 100, 2),
                        rd.Peso
                    }),
                    PorIndicador = resultadosIndicadores.Select(ri => new
                    {
                        ri.Indicador,
                        Puntaje = Math.Round(ri.Puntaje, 4),
                        Porcentaje = ri.Porcentaje,
                        ri.Dimension,
                        ri.Peso
                    }),
                    AccionRecomendada = accionMejora != null ? new
                    {
                        accionMejora.Descripcion,
                        accionMejora.Recomendaciones,
                        Rango = $"{accionMejora.RangoMin}-{accionMejora.RangoMax}"
                    } : null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener resultados: {ex.Message}");
            }
        }


        private bool PreguntasIepmExists(int id)
        {
            return _context.PreguntasIepms.Any(e => e.IdPregunta == id);
        }
    }
}
