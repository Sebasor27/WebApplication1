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
    public class PreguntasIceController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public PreguntasIceController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/PreguntasIce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntasIce>>> GetPreguntasIces()
        {
            return await _context.PreguntasIces.ToListAsync();
        }

        // GET: api/PreguntasIce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreguntasIce>> GetPreguntasIce(int id)
        {
            var preguntasIce = await _context.PreguntasIces.FindAsync(id);

            if (preguntasIce == null)
            {
                return NotFound();
            }

            return preguntasIce;
        }

        // PUT: api/PreguntasIce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreguntasIce(int id, PreguntasIce preguntasIce)
        {
            if (id != preguntasIce.IdPregunta)
            {
                return BadRequest();
            }

            _context.Entry(preguntasIce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntasIceExists(id))
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

        // POST: api/PreguntasIce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PreguntasIce>> PostPreguntasIce(PreguntasIce preguntasIce)
        {
            _context.PreguntasIces.Add(preguntasIce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreguntasIce", new { id = preguntasIce.IdPregunta }, preguntasIce);
        }

        // DELETE: api/PreguntasIce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreguntasIce(int id)
        {
            var preguntasIce = await _context.PreguntasIces.FindAsync(id);
            if (preguntasIce == null)
            {
                return NotFound();
            }

            _context.PreguntasIces.Remove(preguntasIce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreguntasIceExists(int id)
        {
            return _context.PreguntasIces.Any(e => e.IdPregunta == id);
        }
    }
}
