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
    public class EstadisticasEmprendedoresController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public EstadisticasEmprendedoresController(CentroEmpContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetEstadisticasCompletas()
        {
            try
            {
                var emprendedores = await _context.Emprendedores
                    .Where(e => e.Estado) 
                    .ToListAsync();

                if (!emprendedores.Any())
                {
                    return NotFound("No se encontraron emprendedores activos");
                }

                const int cantidadRangosSueldo = 3;
                const int cantidadRangosEdad = 3;

                var sueldos = emprendedores
                    .Select(e => decimal.TryParse(e.SueldoMensual, out var sueldo) ? sueldo : 0)
                    .Where(s => s > 0)
                    .ToList();

                var edades = emprendedores
                    .Select(e => int.TryParse(e.Edad, out var edad) ? edad : 0)
                    .Where(e => e > 0)
                    .ToList();

                var rangosSueldo = GenerarRangosDinamicos(sueldos, cantidadRangosSueldo);
                var rangosEdad = GenerarRangosDinamicos(edades, cantidadRangosEdad);

                var totalEmprendedores = emprendedores.Count;
                var totalEmpleados = emprendedores.Sum(e => e.EmpleadosHombres + e.EmpleadosMujeres);
                var promedioSueldos = sueldos.Any() ? Math.Round(sueldos.Average(), 2) : 0;
                var promedioEdad = edades.Any() ? Math.Round(edades.Average(), 1) : 0;

                var opcionesNivelEstudio = emprendedores
                    .Select(e => e.NivelEstudio)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var opcionesTipoEmpresa = emprendedores
                    .Select(e => e.TipoEmpresa)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var opcionesAnios = emprendedores
                    .Select(e => e.AnoCreacionEmpresa)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var distribucionSueldos = rangosSueldo
                    .Select(rango => new
                    {
                        name = $"{rango.Min}-{rango.Max}",
                        value = emprendedores.Count(e =>
                            decimal.TryParse(e.SueldoMensual, out var sueldo) &&
                            sueldo >= rango.Min &&
                            sueldo <= rango.Max)
                    })
                    .ToList();

                var distribucionEdades = rangosEdad
                    .Select(rango => new
                    {
                        name = $"{rango.Min}-{rango.Max}",
                        value = emprendedores.Count(e =>
                            int.TryParse(e.Edad, out var edad) &&
                            edad >= rango.Min &&
                            edad <= rango.Max)
                    })
                    .ToList();

                var distribucionNivelEstudio = opcionesNivelEstudio
                    .Select(nivel => new
                    {
                        name = nivel,
                        value = emprendedores.Count(e => e.NivelEstudio == nivel)
                    })
                    .ToList();

                var relacionDependencia = new[]
                {
                    new { name = "Dependencia", value = emprendedores.Count(e => e.TrabajoRelacionDependencia) },
                    new { name = "Independiente", value = emprendedores.Count(e => !e.TrabajoRelacionDependencia) }
                };

                var distribucionTipoEmpresa = opcionesTipoEmpresa
                    .Select(tipo => new
                    {
                        name = tipo,
                        value = emprendedores.Count(e => e.TipoEmpresa == tipo)
                    })
                    .ToList();

                var empleadosPorGenero = new[]
                {
                    new { name = "Hombres", value = emprendedores.Sum(e => e.EmpleadosHombres) },
                    new { name = "Mujeres", value = emprendedores.Sum(e => e.EmpleadosMujeres) }
                };

                var evolucionAnual = opcionesAnios
                    .Select(anio => new
                    {
                        anio,
                        emprendedores = emprendedores.Count(e => e.AnoCreacionEmpresa == anio),
                        empleados = emprendedores
                            .Where(e => e.AnoCreacionEmpresa == anio)
                            .Sum(e => e.EmpleadosHombres + e.EmpleadosMujeres),
                        sueldoPromedio = Math.Round(emprendedores
                            .Where(e => e.AnoCreacionEmpresa == anio)
                            .Average(e => decimal.TryParse(e.SueldoMensual, out var s) ? s : 0), 2),
                        edadPromedio = Math.Round(emprendedores
                            .Where(e => e.AnoCreacionEmpresa == anio)
                            .Average(e => int.TryParse(e.Edad, out var edad) ? edad : 0), 1)
                    })
                    .OrderBy(x => x.anio)
                    .ToList();

                var response = new
                {
                    kpis = new
                    {
                        totalEmprendedores,
                        totalEmpleados,
                        promedioSueldos,
                        promedioEdad
                    },
                    filtros = new
                    {
                        opcionesRangoSueldo = distribucionSueldos.Select(x => x.name).ToList(),
                        opcionesRangoEdad = distribucionEdades.Select(x => x.name).ToList(),
                        opcionesNivelEstudio,
                        opcionesTipoEmpresa,
                        opcionesAnios
                    },
                    graficos = new
                    {
                        distribucionSueldos,
                        distribucionEdades,
                        distribucionNivelEstudio,
                        relacionDependencia,
                        distribucionTipoEmpresa,
                        empleadosPorGenero,
                        evolucionAnual
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private List<(decimal Min, decimal Max)> GenerarRangosDinamicos(List<decimal> valores, int cantidadRangos)
        {
            if (valores == null || !valores.Any())
                return new List<(decimal, decimal)> { (0, 0) };

            valores.Sort();
            decimal min = valores.Min();
            decimal max = valores.Max();

            if (min == max)
                return new List<(decimal, decimal)> { (min, max) };

            decimal ajuste = (max - min) * 0.0001m;
            max += ajuste;

            decimal intervalo = (max - min) / cantidadRangos;
            var rangos = new List<(decimal Min, decimal Max)>();

            for (int i = 0; i < cantidadRangos; i++)
            {
                decimal rangoMin = min + intervalo * i;
                decimal rangoMax = (i == cantidadRangos - 1) ? max : min + intervalo * (i + 1);

                rangoMin = Math.Floor(rangoMin / 10) * 10;
                rangoMax = Math.Ceiling(rangoMax / 10) * 10;

                if (i > 0)
                {
                    rangoMin = rangos[i - 1].Max + 1m;
                }

                rangos.Add((Min: rangoMin, Max: rangoMax));
            }

            return rangos;
        }

        private List<(int Min, int Max)> GenerarRangosDinamicos(List<int> valores, int cantidadRangos)
        {
            var decimales = valores.Select(v => (decimal)v).ToList();
            var rangosDecimal = GenerarRangosDinamicos(decimales, cantidadRangos);
            return rangosDecimal.Select(r => ((int)r.Min, (int)r.Max)).ToList();
        }
    }
}