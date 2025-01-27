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
    public class EmprendedoresController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public EmprendedoresController(CentroEmpContext context)
        {
            _context = context;
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
        public async Task<ActionResult<Emprendedore>> PostEmprendedore(Emprendedore emprendedore)
        {
            _context.Emprendedores.Add(emprendedore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmprendedore", new { id = emprendedore.IdEmprendedor }, emprendedore);
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
