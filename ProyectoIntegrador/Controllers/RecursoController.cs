using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.Models;

namespace ProyectoIntegrador.Controllers
{
    public class RecursoController : Controller
    {
        private readonly SportQuitoContext _context;

        public RecursoController(SportQuitoContext context)
        {
            _context = context;
        }

        // GET: Recurso
        public async Task<IActionResult> Index()
        {
              return _context.Recursos != null ? 
                          View(await _context.Recursos.ToListAsync()) :
                          Problem("Entity set 'SportQuitoContext.Recursos'  is null.");
        }

        // GET: Recurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recursos == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(m => m.IdRecurso == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // GET: Recurso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecurso,NombreRecurso,DescripcionRecurso,CantidadRecurso,CostoRecurso,DisponibilidadRecurso")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recurso);
        }

        // GET: Recurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recursos == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }
            return View(recurso);
        }

        // POST: Recurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecurso,NombreRecurso,DescripcionRecurso,CantidadRecurso,CostoRecurso,DisponibilidadRecurso")] Recurso recurso)
        {
            if (id != recurso.IdRecurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursoExists(recurso.IdRecurso))
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
            return View(recurso);
        }

        // GET: Recurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recursos == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(m => m.IdRecurso == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // POST: Recurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recursos == null)
            {
                return Problem("Entity set 'SportQuitoContext.Recursos'  is null.");
            }
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso != null)
            {
                _context.Recursos.Remove(recurso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursoExists(int id)
        {
          return (_context.Recursos?.Any(e => e.IdRecurso == id)).GetValueOrDefault();
        }
    }
}
