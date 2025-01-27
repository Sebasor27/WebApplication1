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
    public class ResultadosIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public ResultadosIepmController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/ResultadosIepm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadosIepm>>> GetResultadosIepms()
        {
            return await _context.ResultadosIepms.ToListAsync();
        }

        // GET: api/ResultadosIepm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultadosIepm>> GetResultadosIepm(int id)
        {
            var resultadosIepm = await _context.ResultadosIepms.FindAsync(id);

            if (resultadosIepm == null)
            {
                return NotFound();
            }

            return resultadosIepm;
        }

        // PUT: api/ResultadosIepm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultadosIepm(int id, ResultadosIepm resultadosIepm)
        {
            if (id != resultadosIepm.IdResultado)
            {
                return BadRequest();
            }

            _context.Entry(resultadosIepm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadosIepmExists(id))
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

        // POST: api/ResultadosIepm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResultadosIepm>> PostResultadosIepm(ResultadosIepm resultadosIepm)
        {
            _context.ResultadosIepms.Add(resultadosIepm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultadosIepm", new { id = resultadosIepm.IdResultado }, resultadosIepm);
        }

        // DELETE: api/ResultadosIepm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultadosIepm(int id)
        {
            var resultadosIepm = await _context.ResultadosIepms.FindAsync(id);
            if (resultadosIepm == null)
            {
                return NotFound();
            }

            _context.ResultadosIepms.Remove(resultadosIepm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultadosIepmExists(int id)
        {
            return _context.ResultadosIepms.Any(e => e.IdResultado == id);
        }
    }
}
