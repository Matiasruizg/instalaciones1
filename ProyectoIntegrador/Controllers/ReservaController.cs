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
    public class ReservaController : Controller
    {
        private readonly SportQuitoContext _context;

        public ReservaController(SportQuitoContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var sportQuitoContext = _context.Reservas.Include(r => r.IdInstalacionDeportivaNavigation).Include(r => r.IdUperfilNavigation);
            return View(await sportQuitoContext.ToListAsync());
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdInstalacionDeportivaNavigation)
                .Include(r => r.IdUperfilNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            ViewData["IdInstalacionDeportiva"] = new SelectList(_context.InstalacionesDeportivas, "IdInstalacionDeportiva", "IdInstalacionDeportiva");
            ViewData["IdUperfil"] = new SelectList(_context.UsuarioPerfils, "IdUperfil", "IdUperfil");
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,CostoReserva,FechaReserva,HoraReserva,IdUperfil,IdInstalacionDeportiva")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInstalacionDeportiva"] = new SelectList(_context.InstalacionesDeportivas, "IdInstalacionDeportiva", "IdInstalacionDeportiva", reserva.IdInstalacionDeportiva);
            ViewData["IdUperfil"] = new SelectList(_context.UsuarioPerfils, "IdUperfil", "IdUperfil", reserva.IdUperfil);
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdInstalacionDeportiva"] = new SelectList(_context.InstalacionesDeportivas, "IdInstalacionDeportiva", "IdInstalacionDeportiva", reserva.IdInstalacionDeportiva);
            ViewData["IdUperfil"] = new SelectList(_context.UsuarioPerfils, "IdUperfil", "IdUperfil", reserva.IdUperfil);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,CostoReserva,FechaReserva,HoraReserva,IdUperfil,IdInstalacionDeportiva")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
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
            ViewData["IdInstalacionDeportiva"] = new SelectList(_context.InstalacionesDeportivas, "IdInstalacionDeportiva", "IdInstalacionDeportiva", reserva.IdInstalacionDeportiva);
            ViewData["IdUperfil"] = new SelectList(_context.UsuarioPerfils, "IdUperfil", "IdUperfil", reserva.IdUperfil);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdInstalacionDeportivaNavigation)
                .Include(r => r.IdUperfilNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'SportQuitoContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reservas?.Any(e => e.IdReserva == id)).GetValueOrDefault();
        }
    }
}
