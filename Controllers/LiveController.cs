using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_lives.Models;
using mvc_lives.Models.Data;

namespace mvc_lives.Controllers
{
    public class LiveController : Controller
    {
        private readonly Contexto _context;

        public LiveController(Contexto context)
        {
            _context = context;
        }

        // GET: Live
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Live.Include(l => l.Instrutor);

            return View(await contexto.ToListAsync());
        }

        // GET: Live/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Live == null)
            {
                return NotFound();
            }

            var live = await _context.Live
                .Include(l => l.Instrutor)
                .FirstOrDefaultAsync(m => m.LiveID == id);
            if (live == null)
            {
                return NotFound();
            }

            return View(live);
        }

        // GET: Live/Create
        public IActionResult Create()
        {
            ViewData["InstrutorID"] = new SelectList(_context.Instrutor, "InstrutorID", "Nome");

            var instrutorNome = _context.Instrutor.Select(l => new
            {
                InstrutorID = l.InstrutorID,
                InstrutorNome = l.Nome
            }).ToList();

            ViewBag.InstrutorNome = new MultiSelectList(instrutorNome, "InstrutorID", "InstrutorNome");

            return View();
        }

        // POST: Live/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LiveID,InstrutorID,Nome,Descricao,HoraInicio,DuracaoMin,ValorInscricao")] Live live)
        {
            if (ModelState.IsValid)
            {
                _context.Add(live);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstrutorID"] = new SelectList(_context.Instrutor, "InstrutorID", "InstrutorID", live.InstrutorID);
            return View(live);
        }

        // GET: Live/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Live == null)
            {
                return NotFound();
            }

            var live = await _context.Live.FindAsync(id);
            if (live == null)
            {
                return NotFound();
            }
            ViewData["InstrutorID"] = new SelectList(_context.Instrutor, "InstrutorID", "InstrutorID", live.InstrutorID);

            var instrutorNome = _context.Instrutor.Select(l => new
            {
                InstrutorID = l.InstrutorID,
                InstrutorNome = l.Nome
            }).ToList();

            ViewBag.InstrutorNome = new MultiSelectList(instrutorNome, "InstrutorID", "InstrutorNome");
            return View(live);
        }

        // POST: Live/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LiveID,InstrutorID,Nome,Descricao,HoraInicio,DuracaoMin,ValorInscricao")] Live live)
        {
            if (id != live.LiveID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(live);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LiveExists(live.LiveID))
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
            ViewData["InstrutorID"] = new SelectList(_context.Instrutor, "InstrutorID", "InstrutorID", live.InstrutorID);
            return View(live);
        }

        // GET: Live/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Live == null)
            {
                return NotFound();
            }

            var live = await _context.Live
                .Include(l => l.Instrutor)
                .FirstOrDefaultAsync(m => m.LiveID == id);
            if (live == null)
            {
                return NotFound();
            }

            return View(live);
        }

        // POST: Live/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Live == null)
            {
                return Problem("Entity set 'Contexto.Live'  is null.");
            }
            var live = await _context.Live.FindAsync(id);
            if (live != null)
            {
                _context.Live.Remove(live);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LiveExists(int id)
        {
            return (_context.Live?.Any(e => e.LiveID == id)).GetValueOrDefault();
        }
    }
}
