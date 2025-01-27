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
    public class EncuestasIceController : ControllerBase
    {
        private readonly CentroEmpContext _context;

        public EncuestasIceController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: api/EncuestasIce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestasIce>>> GetEncuestasIces()
        {
            return await _context.EncuestasIces.ToListAsync();
        }

        // GET: api/EncuestasIce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncuestasIce>> GetEncuestasIce(int id)
        {
            var encuestasIce = await _context.EncuestasIces.FindAsync(id);

            if (encuestasIce == null)
            {
                return NotFound();
            }

            return encuestasIce;
        }

        // PUT: api/EncuestasIce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuestasIce(int id, EncuestasIce encuestasIce)
        {
            if (id != encuestasIce.IdRespuesta)
            {
                return BadRequest();
            }

            _context.Entry(encuestasIce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestasIceExists(id))
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

        // POST: api/EncuestasIce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<EncuestasIce>>> PostEncuestasIce([FromBody] List<EncuestasIce> encuestasIce)
        {
            if (encuestasIce == null || !encuestasIce.Any())
            {
                return BadRequest("No se han enviado respuestas.");
            }

            _context.EncuestasIces.AddRange(encuestasIce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncuestasIce", new { id = encuestasIce.First().IdRespuesta }, encuestasIce);
        }


        private bool EncuestasIceExists(int id)
        {
            return _context.EncuestasIces.Any(e => e.IdRespuesta == id);
        }
    }
}
