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
    public class PagoController : Controller
    {
        private readonly SportQuitoContext _context;

        public PagoController(SportQuitoContext context)
        {
            _context = context;
        }

        // GET: Pago
        public async Task<IActionResult> Index()
        {
            var sportQuitoContext = _context.Pagos.Include(p => p.IdReservaNavigation).Include(p => p.IdTipoPagoNavigation);
            return View(await sportQuitoContext.ToListAsync());
        }

        // GET: Pago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdReservaNavigation)
                .Include(p => p.IdTipoPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdPago == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pago/Create
        public IActionResult Create()
        {
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva");
            ViewData["IdTipoPago"] = new SelectList(_context.TipoPagos, "IdTipoPago", "IdTipoPago");
            return View();
        }

        // POST: Pago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPago,MontoPago,FechaPago,HoraPago,TotalPago,IdReserva,IdTipoPago")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", pago.IdReserva);
            ViewData["IdTipoPago"] = new SelectList(_context.TipoPagos, "IdTipoPago", "IdTipoPago", pago.IdTipoPago);
            return View(pago);
        }

        // GET: Pago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", pago.IdReserva);
            ViewData["IdTipoPago"] = new SelectList(_context.TipoPagos, "IdTipoPago", "IdTipoPago", pago.IdTipoPago);
            return View(pago);
        }

        // POST: Pago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPago,MontoPago,FechaPago,HoraPago,TotalPago,IdReserva,IdTipoPago")] Pago pago)
        {
            if (id != pago.IdPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.IdPago))
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
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", pago.IdReserva);
            ViewData["IdTipoPago"] = new SelectList(_context.TipoPagos, "IdTipoPago", "IdTipoPago", pago.IdTipoPago);
            return View(pago);
        }

        // GET: Pago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdReservaNavigation)
                .Include(p => p.IdTipoPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdPago == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pagos == null)
            {
                return Problem("Entity set 'SportQuitoContext.Pagos'  is null.");
            }
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
          return (_context.Pagos?.Any(e => e.IdPago == id)).GetValueOrDefault();
        }
    }
}
