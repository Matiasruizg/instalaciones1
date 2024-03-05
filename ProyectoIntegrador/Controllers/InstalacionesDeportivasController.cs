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
    public class InstalacionesDeportivasController : Controller
    {
        private readonly SportQuitoContext _context;

        public InstalacionesDeportivasController(SportQuitoContext context)
        {
            _context = context;
        }

        // GET: InstalacionesDeportivas
        public async Task<IActionResult> Index()
        {
              return _context.InstalacionesDeportivas != null ? 
                          View(await _context.InstalacionesDeportivas.ToListAsync()) :
                          Problem("Entity set 'SportQuitoContext.InstalacionesDeportivas'  is null.");
        }

        // GET: InstalacionesDeportivas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InstalacionesDeportivas == null)
            {
                return NotFound();
            }

            var instalacionesDeportiva = await _context.InstalacionesDeportivas
                .FirstOrDefaultAsync(m => m.IdInstalacionDeportiva == id);
            if (instalacionesDeportiva == null)
            {
                return NotFound();
            }

            return View(instalacionesDeportiva);
        }

        // GET: InstalacionesDeportivas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstalacionesDeportivas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInstalacionDeportiva,NombreInstalacion,TipoCanchaInstalacion,UbicacionInstalacion,DisponibilidadInstalacion")] InstalacionesDeportiva instalacionesDeportiva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instalacionesDeportiva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instalacionesDeportiva);
        }

        // GET: InstalacionesDeportivas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InstalacionesDeportivas == null)
            {
                return NotFound();
            }

            var instalacionesDeportiva = await _context.InstalacionesDeportivas.FindAsync(id);
            if (instalacionesDeportiva == null)
            {
                return NotFound();
            }
            return View(instalacionesDeportiva);
        }

        // POST: InstalacionesDeportivas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstalacionDeportiva,NombreInstalacion,TipoCanchaInstalacion,UbicacionInstalacion,DisponibilidadInstalacion")] InstalacionesDeportiva instalacionesDeportiva)
        {
            if (id != instalacionesDeportiva.IdInstalacionDeportiva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instalacionesDeportiva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstalacionesDeportivaExists(instalacionesDeportiva.IdInstalacionDeportiva))
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
            return View(instalacionesDeportiva);
        }

        // GET: InstalacionesDeportivas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InstalacionesDeportivas == null)
            {
                return NotFound();
            }

            var instalacionesDeportiva = await _context.InstalacionesDeportivas
                .FirstOrDefaultAsync(m => m.IdInstalacionDeportiva == id);
            if (instalacionesDeportiva == null)
            {
                return NotFound();
            }

            return View(instalacionesDeportiva);
        }

        // POST: InstalacionesDeportivas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InstalacionesDeportivas == null)
            {
                return Problem("Entity set 'SportQuitoContext.InstalacionesDeportivas'  is null.");
            }
            var instalacionesDeportiva = await _context.InstalacionesDeportivas.FindAsync(id);
            if (instalacionesDeportiva != null)
            {
                _context.InstalacionesDeportivas.Remove(instalacionesDeportiva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstalacionesDeportivaExists(int id)
        {
          return (_context.InstalacionesDeportivas?.Any(e => e.IdInstalacionDeportiva == id)).GetValueOrDefault();
        }
    }
}
