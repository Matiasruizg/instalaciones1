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
    public class UsuarioPerfilController : Controller
    {
        private readonly SportQuitoContext _context;

        public UsuarioPerfilController(SportQuitoContext context)
        {
            _context = context;
        }

        // GET: UsuarioPerfil
        public async Task<IActionResult> Index()
        {
            var sportQuitoContext = _context.UsuarioPerfils.Include(u => u.IdPerfilNavigation).Include(u => u.IdUsuarioNavigation);
            return View(await sportQuitoContext.ToListAsync());
        }

        // GET: UsuarioPerfil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils
                .Include(u => u.IdPerfilNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdUperfil == id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }

            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Create
        public IActionResult Create()
        {
            ViewData["IdPerfil"] = new SelectList(_context.Perfils, "IdPerfil", "IdPerfil");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: UsuarioPerfil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUperfil,IdUsuario,IdPerfil")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioPerfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfils, "IdPerfil", "IdPerfil", usuarioPerfil.IdPerfil);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", usuarioPerfil.IdUsuario);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils.FindAsync(id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfils, "IdPerfil", "IdPerfil", usuarioPerfil.IdPerfil);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", usuarioPerfil.IdUsuario);
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUperfil,IdUsuario,IdPerfil")] UsuarioPerfil usuarioPerfil)
        {
            if (id != usuarioPerfil.IdUperfil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioPerfil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioPerfilExists(usuarioPerfil.IdUperfil))
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
            ViewData["IdPerfil"] = new SelectList(_context.Perfils, "IdPerfil", "IdPerfil", usuarioPerfil.IdPerfil);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", usuarioPerfil.IdUsuario);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils
                .Include(u => u.IdPerfilNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdUperfil == id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }

            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioPerfils == null)
            {
                return Problem("Entity set 'SportQuitoContext.UsuarioPerfils'  is null.");
            }
            var usuarioPerfil = await _context.UsuarioPerfils.FindAsync(id);
            if (usuarioPerfil != null)
            {
                _context.UsuarioPerfils.Remove(usuarioPerfil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioPerfilExists(int id)
        {
          return (_context.UsuarioPerfils?.Any(e => e.IdUperfil == id)).GetValueOrDefault();
        }
    }
}
