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

        // GET: api/PreguntasIepm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntasIepm>>> GetPreguntasIepms()
        {
            return await _context.PreguntasIepms.ToListAsync();
        }

        // GET: api/PreguntasIepm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreguntasIepm>> GetPreguntasIepm(int id)
        {
            var preguntasIepm = await _context.PreguntasIepms.FindAsync(id);

            if (preguntasIepm == null)
            {
                return NotFound();
            }

            return preguntasIepm;
        }

        // PUT: api/PreguntasIepm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreguntasIepm(int id, PreguntasIepm preguntasIepm)
        {
            if (id != preguntasIepm.IdPregunta)
            {
                return BadRequest();
            }

            _context.Entry(preguntasIepm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntasIepmExists(id))
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

        // POST: api/PreguntasIepm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PreguntasIepm>> PostPreguntasIepm(PreguntasIepm preguntasIepm)
        {
            _context.PreguntasIepms.Add(preguntasIepm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreguntasIepm", new { id = preguntasIepm.IdPregunta }, preguntasIepm);
        }

        // DELETE: api/PreguntasIepm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreguntasIepm(int id)
        {
            var preguntasIepm = await _context.PreguntasIepms.FindAsync(id);
            if (preguntasIepm == null)
            {
                return NotFound();
            }

            _context.PreguntasIepms.Remove(preguntasIepm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreguntasIepmExists(int id)
        {
            return _context.PreguntasIepms.Any(e => e.IdPregunta == id);
        }
    }
}
