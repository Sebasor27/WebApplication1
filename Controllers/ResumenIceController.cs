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
    public class ResumenIceController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public ResumenIceController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/ResumenIce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumenIce>>> GetResumenIces()
        {
            return await _context.ResumenIces.ToListAsync();
        }

        // GET: api/ResumenIce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumenIce>> GetResumenIce(int id)
        {
            var resumenIce = await _context.ResumenIces.FindAsync(id);

            if (resumenIce == null)
            {
                return NotFound();
            }

            return resumenIce;
        }

        // PUT: api/ResumenIce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResumenIce(int id, ResumenIce resumenIce)
        {
            if (id != resumenIce.IdEmprendedor)
            {
                return BadRequest();
            }

            _context.Entry(resumenIce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumenIceExists(id))
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

        // POST: api/ResumenIce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResumenIce>> PostResumenIce(ResumenIce resumenIce)
        {
            _context.ResumenIces.Add(resumenIce);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ResumenIceExists(resumenIce.IdEmprendedor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetResumenIce", new { id = resumenIce.IdEmprendedor }, resumenIce);
        }

        // DELETE: api/ResumenIce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResumenIce(int id)
        {
            var resumenIce = await _context.ResumenIces.FindAsync(id);
            if (resumenIce == null)
            {
                return NotFound();
            }

            _context.ResumenIces.Remove(resumenIce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResumenIceExists(int id)
        {
            return _context.ResumenIces.Any(e => e.IdEmprendedor == id);
        }
    }
}
