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
    public class ResultadosIceController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public ResultadosIceController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/ResultadosIce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadosIce>>> GetResultadosIces()
        {
            return await _context.ResultadosIces.ToListAsync();
        }

        // GET: api/ResultadosIce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultadosIce>> GetResultadosIce(int id)
        {
            var resultadosIce = await _context.ResultadosIces.FindAsync(id);

            if (resultadosIce == null)
            {
                return NotFound();
            }

            return resultadosIce;
        }

        // PUT: api/ResultadosIce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultadosIce(int id, ResultadosIce resultadosIce)
        {
            if (id != resultadosIce.IdResultado)
            {
                return BadRequest();
            }

            _context.Entry(resultadosIce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadosIceExists(id))
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

        // POST: api/ResultadosIce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResultadosIce>> PostResultadosIce(ResultadosIce resultadosIce)
        {
            _context.ResultadosIces.Add(resultadosIce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultadosIce", new { id = resultadosIce.IdResultado }, resultadosIce);
        }

        // DELETE: api/ResultadosIce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultadosIce(int id)
        {
            var resultadosIce = await _context.ResultadosIces.FindAsync(id);
            if (resultadosIce == null)
            {
                return NotFound();
            }

            _context.ResultadosIces.Remove(resultadosIce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultadosIceExists(int id)
        {
            return _context.ResultadosIces.Any(e => e.IdResultado == id);
        }
    }
}
