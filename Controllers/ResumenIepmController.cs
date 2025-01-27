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
    public class ResumenIepmController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public ResumenIepmController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/ResumenIepm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumenIepm>>> GetResumenIepms()
        {
            return await _context.ResumenIepms.ToListAsync();
        }

        // GET: api/ResumenIepm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumenIepm>> GetResumenIepm(int id)
        {
            var resumenIepm = await _context.ResumenIepms.FindAsync(id);

            if (resumenIepm == null)
            {
                return NotFound();
            }

            return resumenIepm;
        }

        // PUT: api/ResumenIepm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResumenIepm(int id, ResumenIepm resumenIepm)
        {
            if (id != resumenIepm.IdEmprendedor)
            {
                return BadRequest();
            }

            _context.Entry(resumenIepm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumenIepmExists(id))
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

        // POST: api/ResumenIepm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResumenIepm>> PostResumenIepm(ResumenIepm resumenIepm)
        {
            _context.ResumenIepms.Add(resumenIepm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ResumenIepmExists(resumenIepm.IdEmprendedor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetResumenIepm", new { id = resumenIepm.IdEmprendedor }, resumenIepm);
        }

        // DELETE: api/ResumenIepm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResumenIepm(int id)
        {
            var resumenIepm = await _context.ResumenIepms.FindAsync(id);
            if (resumenIepm == null)
            {
                return NotFound();
            }

            _context.ResumenIepms.Remove(resumenIepm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResumenIepmExists(int id)
        {
            return _context.ResumenIepms.Any(e => e.IdEmprendedor == id);
        }
    }
}
