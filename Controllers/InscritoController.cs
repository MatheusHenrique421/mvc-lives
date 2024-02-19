using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_lives.Models;
using mvc_lives.Models.Data;

namespace mvc_lives.Controllers
{
    public class InscritoController : Controller
    {
        private readonly Contexto _context;

        public InscritoController(Contexto context)
        {
            _context = context;
        }

        // GET: Inscrito
        public async Task<IActionResult> Index()
        {
              return _context.Inscrito != null ? 
                          View(await _context.Inscrito.ToListAsync()) :
                          Problem("Entity set 'Contexto.Inscrito'  is null.");
        }

        // GET: Inscrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inscrito == null)
            {
                return NotFound();
            }

            var inscrito = await _context.Inscrito
                .FirstOrDefaultAsync(m => m.InscritoID == id);
            if (inscrito == null)
            {
                return NotFound();
            }

            return View(inscrito);
        }

        // GET: Inscrito/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inscrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscritoID,Nome,DataNascimento,Email,Instagram")] Inscrito inscrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inscrito);
        }

        // GET: Inscrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inscrito == null)
            {
                return NotFound();
            }

            var inscrito = await _context.Inscrito.FindAsync(id);
            if (inscrito == null)
            {
                return NotFound();
            }
            return View(inscrito);
        }

        // POST: Inscrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscritoID,Nome,DataNascimento,Email,Instagram")] Inscrito inscrito)
        {
            if (id != inscrito.InscritoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscritoExists(inscrito.InscritoID))
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
            return View(inscrito);
        }

        // GET: Inscrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inscrito == null)
            {
                return NotFound();
            }

            var inscrito = await _context.Inscrito
                .FirstOrDefaultAsync(m => m.InscritoID == id);
            if (inscrito == null)
            {
                return NotFound();
            }

            return View(inscrito);
        }

        // POST: Inscrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inscrito == null)
            {
                return Problem("Entity set 'Contexto.Inscrito'  is null.");
            }
            var inscrito = await _context.Inscrito.FindAsync(id);
            if (inscrito != null)
            {
                _context.Inscrito.Remove(inscrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscritoExists(int id)
        {
          return (_context.Inscrito?.Any(e => e.InscritoID == id)).GetValueOrDefault();
        }
    }
}
