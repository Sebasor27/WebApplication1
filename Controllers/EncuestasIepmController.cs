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
    public class EncuestasIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public EncuestasIepmController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/EncuestasIepm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestasIepm>>> GetEncuestasIepms()
        {
            return await _context.EncuestasIepms.ToListAsync();
        }

        // GET: api/EncuestasIepm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncuestasIepm>> GetEncuestasIepm(int id)
        {
            var encuestasIepm = await _context.EncuestasIepms.FindAsync(id);

            if (encuestasIepm == null)
            {
                return NotFound();
            }

            return encuestasIepm;
        }

        // PUT: api/EncuestasIepm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuestasIepm(int id, EncuestasIepm encuestasIepm)
        {
            if (id != encuestasIepm.IdRespuesta)
            {
                return BadRequest();
            }

            _context.Entry(encuestasIepm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestasIepmExists(id))
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

        // POST: api/EncuestasIepm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EncuestasIepm>> PostEncuestasIepm(EncuestasIepm encuestasIepm)
        {
            _context.EncuestasIepms.Add(encuestasIepm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncuestasIepm", new { id = encuestasIepm.IdRespuesta }, encuestasIepm);
        }

        // DELETE: api/EncuestasIepm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncuestasIepm(int id)
        {
            var encuestasIepm = await _context.EncuestasIepms.FindAsync(id);
            if (encuestasIepm == null)
            {
                return NotFound();
            }

            _context.EncuestasIepms.Remove(encuestasIepm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncuestasIepmExists(int id)
        {
            return _context.EncuestasIepms.Any(e => e.IdRespuesta == id);
        }
    }
}
