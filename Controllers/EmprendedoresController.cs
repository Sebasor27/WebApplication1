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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprendedore(int id, Emprendedore emprendedore)
        {
            if (id != emprendedore.IdEmprendedor)
            {
                return BadRequest();
            }

            _context.Entry(emprendedore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprendedoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Emprendedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
            FechaRegistro = createDto.FechaRegistro
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
            FechaRegistro = emprendedor.FechaRegistro
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
            var emprendedore = await _context.Emprendedores.FindAsync(id);
            if (emprendedore == null)
            {
                return NotFound();
            }

            _context.Emprendedores.Remove(emprendedore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmprendedoreExists(int id)
        {
            return _context.Emprendedores.Any(e => e.IdEmprendedor == id);
        }
    }
}
