using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenciaController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public CompetenciaController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/Competencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competencia>>> GetCompetencias()
        {
            return await _context.Competencias.ToListAsync();
        }

        // GET: api/Competencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Competencia>> GetCompetencia(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);

            if (competencia == null)
            {
                return NotFound();
            }

            return competencia;
        }

        // POST: api/Competencia
        [HttpPost]
        public async Task<ActionResult<Competencia>> PostCompetencia(Competencia competencia)
        {
            _context.Competencias.Add(competencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompetencia), new { id = competencia.IdCompetencia }, competencia);
        }

        // PUT: api/Competencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetencia(int id, Competencia competencia)
        {
            if (id != competencia.IdCompetencia)
            {
                return BadRequest();
            }

            _context.Entry(competencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenciaExists(id))
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

        // DELETE: api/Competencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetencia(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);
            if (competencia == null)
            {
                return NotFound();
            }

            _context.Competencias.Remove(competencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetenciaExists(int id)
        {
            return _context.Competencias.Any(e => e.IdCompetencia == id);
        }
    }
}
