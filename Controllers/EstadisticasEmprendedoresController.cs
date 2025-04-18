using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

                var rangosSueldo = new List<(decimal Min, decimal Max, string Nombre)>
        {
            (0, 460, "0-460"),
            (460, 750, "460-750"),
            (750, 1000, "750-1000"),
            (1000, decimal.MaxValue, "1000+")
        };

                var sueldosPuntosMedios = emprendedores
                    .Select(e =>
                    {
                        if (string.IsNullOrEmpty(e.SueldoMensual)) return 0m;

                        if (e.SueldoMensual.Contains("-"))
                        {
                            var partes = e.SueldoMensual.Split('-');
                            if (partes.Length == 2 &&
                                decimal.TryParse(partes[0].Trim(), out var min) &&
                                decimal.TryParse(partes[1].Trim(), out var max))
                            {
                                return (min + max) / 2; 
                            }
                        }
                        else if (decimal.TryParse(e.SueldoMensual, out var valor))
                        {
                            return valor;
                        }
                        return 0m;
                    })
                    .Where(s => s > 0)
                    .ToList();

                var edades = emprendedores
                    .Select(e => int.TryParse(e.Edad, out var edad) ? edad : 0)
                    .Where(e => e > 0)
                    .ToList();

                var totalEmprendedores = emprendedores.Count;
                var totalEmpleados = emprendedores.Sum(e => e.EmpleadosHombres + e.EmpleadosMujeres);
                var promedioSueldos = sueldosPuntosMedios.Any() ? Math.Round(sueldosPuntosMedios.Average(), 2) : 0;
                var promedioEdad = edades.Any() ? Math.Round(edades.Average(), 1) : 0;

                var opcionesNivelEstudio = emprendedores
                    .Select(e => e.NivelEstudio)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var opcionesTipoEmpresa = emprendedores
                    .Select(e => e.TipoEmpresa)
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var opcionesAnios = emprendedores
                    .Select(e => e.AnoCreacionEmpresa)
                    .Where(a => a != null)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var distribucionSueldos = rangosSueldo
                    .Select(rango => new
                    {
                        name = rango.Nombre,
                        value = emprendedores.Count(e =>
                        {
                            if (string.IsNullOrEmpty(e.SueldoMensual)) return false;

                            if (e.SueldoMensual.Contains("-"))
                            {
                                var partes = e.SueldoMensual.Split('-');
                                if (partes.Length == 2 &&
                                    decimal.TryParse(partes[0].Trim(), out var min) &&
                                    decimal.TryParse(partes[1].Trim(), out var max))
                                {
                                    var puntoMedio = (min + max) / 2;
                                    return puntoMedio >= rango.Min &&
                                           (rango.Max == decimal.MaxValue || puntoMedio <= rango.Max);
                                }
                            }
                            else if (decimal.TryParse(e.SueldoMensual, out var valor))
                            {
                                return valor >= rango.Min &&
                                       (rango.Max == decimal.MaxValue || valor <= rango.Max);
                            }
                            return false;
                        })
                    })
                    .ToList();

                var rangosEdad = GenerarRangosEdadDinamicos(edades, 3);

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
                            .Select(e =>
                            {
                                if (string.IsNullOrEmpty(e.SueldoMensual)) return 0m;

                                if (e.SueldoMensual.Contains("-"))
                                {
                                    var partes = e.SueldoMensual.Split('-');
                                    if (partes.Length == 2 &&
                                        decimal.TryParse(partes[0].Trim(), out var min) &&
                                        decimal.TryParse(partes[1].Trim(), out var max))
                                    {
                                        return (min + max) / 2;
                                    }
                                }
                                else if (decimal.TryParse(e.SueldoMensual, out var valor))
                                {
                                    return valor;
                                }
                                return 0m;
                            })
                            .DefaultIfEmpty(0)
                            .Average(), 2),
                        edadPromedio = Math.Round(emprendedores
                            .Where(e => e.AnoCreacionEmpresa == anio)
                            .Select(e => int.TryParse(e.Edad, out var edad) ? edad : 0)
                            .DefaultIfEmpty(0)
                            .Average(), 1)
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

        private List<(int Min, int Max)> GenerarRangosEdadDinamicos(List<int> edades, int cantidadRangos)
        {
            if (!edades.Any()) return new List<(int, int)> { (0, 0) };

            edades.Sort();
            int min = edades.Min();
            int max = edades.Max();

            if (min == max) return new List<(int, int)> { (min, max) };

            if ((max - min) / cantidadRangos < 5)
            {
                max = min + cantidadRangos * 5;
            }

            int tamañoRango = (max - min) / cantidadRangos;
            var rangos = new List<(int Min, int Max)>();

            for (int i = 0; i < cantidadRangos; i++)
            {
                int rangoMin = min + i * tamañoRango;
                int rangoMax = (i == cantidadRangos - 1) ? max : rangoMin + tamañoRango - 1;

                if (i > 0)
                {
                    rangoMin = rangos[i - 1].Max + 1;
                }

                rangos.Add((rangoMin, rangoMax));
            }

            return rangos;
        }
    }
}