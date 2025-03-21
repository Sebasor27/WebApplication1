using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CompetenciaController : Controller
    {
        private readonly CentroEmpContext _context;

        public CompetenciaController(CentroEmpContext context)
        {
            _context = context;
        }

        // GET: Competencia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Competencias.ToListAsync());
        }

        // GET: Competencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencia = await _context.Competencias
                .FirstOrDefaultAsync(m => m.IdCompetencia == id);
            if (competencia == null)
            {
                return NotFound();
            }

            return View(competencia);
        }

        // GET: Competencia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompetencia,NombreCompetencia,PuntosMaximos,PesoRelativo")] Competencia competencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competencia);
        }

        // GET: Competencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencia = await _context.Competencias.FindAsync(id);
            if (competencia == null)
            {
                return NotFound();
            }
            return View(competencia);
        }

        // POST: Competencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompetencia,NombreCompetencia,PuntosMaximos,PesoRelativo")] Competencia competencia)
        {
            if (id != competencia.IdCompetencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetenciaExists(competencia.IdCompetencia))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(competencia);
        }

        // GET: Competencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencia = await _context.Competencias
                .FirstOrDefaultAsync(m => m.IdCompetencia == id);
            if (competencia == null)
            {
                return NotFound();
            }

            return View(competencia);
        }

        // POST: Competencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);
            if (competencia != null)
            {
                _context.Competencias.Remove(competencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetenciaExists(int id)
        {
            return _context.Competencias.Any(e => e.IdCompetencia == id);
        }
    }
}
