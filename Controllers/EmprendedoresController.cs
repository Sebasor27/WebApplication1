using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprendedoresController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public EmprendedoresController(CentroEmpContext context)
        {
            _context = context;
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "idUsuario");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Usuario no válido");
            }
            return userId;
        }

        // GET: api/Emprendedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprendedore>>> GetEmprendedores()
        {
            return await _context.Emprendedores.ToListAsync();
        }

        // GET: api/Emprendedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprendedore>> GetEmprendedore(int id)
        {
            var emprendedore = await _context.Emprendedores.FindAsync(id);

            if (emprendedore == null)
            {
                return NotFound();
            }

            return emprendedore;
        }

        // PUT: api/Emprendedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Emprendedores/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EmprendedorResponseDto>> UpdateEmprendedor(
    int id,
    [FromBody] EmprendedorUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 1. Buscar emprendedor existente
                var emprendedor = await _context.Emprendedores.FindAsync(id);
                if (emprendedor == null)
                {
                    return NotFound();
                }

                // 4. Actualización condicional (mapeo completo)
                if (!string.IsNullOrEmpty(updateDto.Nombre))
                    emprendedor.Nombre = updateDto.Nombre;

                if (!string.IsNullOrEmpty(updateDto.Edad))
                    emprendedor.Edad = updateDto.Edad;

                if (!string.IsNullOrEmpty(updateDto.NivelEstudio))
                    emprendedor.NivelEstudio = updateDto.NivelEstudio;

                if (updateDto.TrabajoRelacionDependencia.HasValue)
                    emprendedor.TrabajoRelacionDependencia = updateDto.TrabajoRelacionDependencia.Value;

                if (!string.IsNullOrEmpty(updateDto.SueldoMensual))
                    emprendedor.SueldoMensual = updateDto.SueldoMensual;

                if (!string.IsNullOrEmpty(updateDto.Ruc))
                    emprendedor.Ruc = updateDto.Ruc;

                if (updateDto.EmpleadosHombres.HasValue)
                    emprendedor.EmpleadosHombres = updateDto.EmpleadosHombres.Value;

                if (updateDto.EmpleadosMujeres.HasValue)
                    emprendedor.EmpleadosMujeres = updateDto.EmpleadosMujeres.Value;

                if (!string.IsNullOrEmpty(updateDto.RangoEdadEmpleados))
                    emprendedor.RangoEdadEmpleados = updateDto.RangoEdadEmpleados;

                if (!string.IsNullOrEmpty(updateDto.TipoEmpresa))
                    emprendedor.TipoEmpresa = updateDto.TipoEmpresa;

                if (updateDto.AnoCreacionEmpresa.HasValue)
                    emprendedor.AnoCreacionEmpresa = updateDto.AnoCreacionEmpresa.Value;

                if (!string.IsNullOrEmpty(updateDto.Direccion))
                    emprendedor.Direccion = updateDto.Direccion;

                if (!string.IsNullOrEmpty(updateDto.Telefono))
                    emprendedor.Telefono = updateDto.Telefono;

                if (!string.IsNullOrEmpty(updateDto.Celular))
                    emprendedor.Celular = updateDto.Celular;

                if (!string.IsNullOrEmpty(updateDto.Correo))
                    emprendedor.Correo = updateDto.Correo;

                if (!string.IsNullOrEmpty(updateDto.Cedula))
                    emprendedor.Cedula = updateDto.Cedula;

                // 5. Guardar cambios
                _context.Entry(emprendedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // 6. Retornar respuesta
                return Ok(new EmprendedorResponseDto
                {
                    IdEmprendedor = emprendedor.IdEmprendedor,
                    IdUsuario = emprendedor.IdUsuario,
                    Nombre = emprendedor.Nombre,
                    Edad = emprendedor.Edad,
                    NivelEstudio = emprendedor.NivelEstudio,
                    TrabajoRelacionDependencia = emprendedor.TrabajoRelacionDependencia,
                    SueldoMensual = emprendedor.SueldoMensual,
                    Ruc = emprendedor.Ruc,
                    EmpleadosHombres = emprendedor.EmpleadosHombres,
                    EmpleadosMujeres = emprendedor.EmpleadosMujeres,
                    RangoEdadEmpleados = emprendedor.RangoEdadEmpleados,
                    TipoEmpresa = emprendedor.TipoEmpresa,
                    AnoCreacionEmpresa = emprendedor.AnoCreacionEmpresa,
                    Direccion = emprendedor.Direccion,
                    Telefono = emprendedor.Telefono,
                    Celular = emprendedor.Celular,
                    Correo = emprendedor.Correo,
                    Cedula = emprendedor.Cedula,
                    FechaRegistro = emprendedor.FechaRegistro,
                    Estado = emprendedor.Estado,
                    FechaInactivacion = emprendedor.FechaInactivacion
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmprendedorResponseDto>> CreateEmprendedor(
    [FromBody] EmprendedorDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = GetUserIdFromToken();

                var emprendedor = new Emprendedore
                {
                    IdUsuario = userId,
                    Nombre = createDto.Nombre,
                    Edad = createDto.Edad,
                    NivelEstudio = createDto.NivelEstudio,
                    TrabajoRelacionDependencia = createDto.TrabajoRelacionDependencia,
                    SueldoMensual = createDto.SueldoMensual,
                    Ruc = createDto.Ruc,
                    EmpleadosHombres = createDto.EmpleadosHombres,
                    EmpleadosMujeres = createDto.EmpleadosMujeres,
                    RangoEdadEmpleados = createDto.RangoEdadEmpleados,
                    TipoEmpresa = createDto.TipoEmpresa,
                    AnoCreacionEmpresa = createDto.AnoCreacionEmpresa,
                    Direccion = createDto.Direccion,
                    Telefono = createDto.Telefono,
                    Celular = createDto.Celular,
                    Correo = createDto.Correo,
                    Cedula = createDto.Cedula,
                    FechaRegistro = createDto.FechaRegistro,
                    Estado = true,
                    FechaInactivacion = null
                };

                _context.Emprendedores.Add(emprendedor);
                await _context.SaveChangesAsync();

                var responseDto = new EmprendedorResponseDto
                {
                    IdEmprendedor = emprendedor.IdEmprendedor,
                    IdUsuario = emprendedor.IdUsuario,
                    Nombre = emprendedor.Nombre,
                    Edad = emprendedor.Edad,
                    NivelEstudio = emprendedor.NivelEstudio,
                    TrabajoRelacionDependencia = emprendedor.TrabajoRelacionDependencia,
                    SueldoMensual = emprendedor.SueldoMensual,
                    Ruc = emprendedor.Ruc,
                    EmpleadosHombres = emprendedor.EmpleadosHombres,
                    EmpleadosMujeres = emprendedor.EmpleadosMujeres,
                    RangoEdadEmpleados = emprendedor.RangoEdadEmpleados,
                    TipoEmpresa = emprendedor.TipoEmpresa,
                    AnoCreacionEmpresa = emprendedor.AnoCreacionEmpresa,
                    Direccion = emprendedor.Direccion,
                    Telefono = emprendedor.Telefono,
                    Celular = emprendedor.Celular,
                    Correo = emprendedor.Correo,
                    Cedula = emprendedor.Cedula,
                    FechaRegistro = emprendedor.FechaRegistro,
                    Estado = emprendedor.Estado,
                    FechaInactivacion = emprendedor.FechaInactivacion

                };

                return CreatedAtAction(nameof(GetEmprendedore),
                    new { id = emprendedor.IdEmprendedor }, responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // DELETE: api/Emprendedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprendedore(int id)
        {
            try
            {
                var emprendedor = await _context.Emprendedores.FindAsync(id);
                if (emprendedor == null)
                {
                    return NotFound();
                }

                // En lugar de eliminar, marcamos como inactivo
                emprendedor.Estado = false;
                emprendedor.FechaInactivacion = DateTime.Now;

                _context.Entry(emprendedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al desactivar el emprendedor: {ex.Message}");
            }
        }

        private bool EmprendedoreExists(int id)
        {
            return _context.Emprendedores.Any(e => e.IdEmprendedor == id);
        }
    }
}
