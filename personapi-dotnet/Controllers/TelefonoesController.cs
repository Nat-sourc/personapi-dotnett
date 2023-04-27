using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class TelefonoesController : Controller
    {
        private readonly SumaDbContext _context;

        public TelefonoesController(SumaDbContext context)
        {
            _context = context;
        }

        // GET: Telefonoes
        public async Task<IActionResult> Index()
        {
            var sumaDbContext = _context.Telefonos.Include(t => t.DuenioNavigation);
            return View(await sumaDbContext.ToListAsync());
        }

        // GET: Telefonoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonoes/Create
        public IActionResult Create()
        {
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc");
            return View();
        }

        // POST: Telefonoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Operador,Duenio")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Operador,Duenio")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Num))
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
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Telefonos == null)
            {
                return Problem("Entity set 'SumaDbContext.Telefonos'  is null.");
            }
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
          return (_context.Telefonos?.Any(e => e.Num == id)).GetValueOrDefault();
        }
    }
}
