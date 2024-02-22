using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using mvc_lives.Models.Data;
using mvc_lives.Models;


namespace mvc_lives.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly Contexto _context;

        public InscricaoController(Contexto context)
        {
            _context = context;
        }

        // GET: Inscricao
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Inscricoes.Include(i => i.Inscrito).Include(i => i.Live);

            return View(await contexto.ToListAsync());
        }

        // GET: Inscricao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inscricoes == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes
                .Include(i => i.Inscrito)
                .Include(i => i.Live)
                .FirstOrDefaultAsync(m => m.InscricaoID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            var liveNome = _context.Live.Select(l => new
            {
                LiveID = l.LiveID,
                LiveNome = l.Nome
            }).ToList();

            var inscritoNome = _context.Inscrito.Select(i => new
            {
                InscritoID = i.InscritoID,
                InscritoNome = i.Nome
            }).ToList();

            ViewBag.LiveNome = new MultiSelectList(liveNome, "LiveID", "LiveNome");
            ViewBag.InscritoNome = new MultiSelectList(inscritoNome, "InscritoID", "InscritoNome");

            return View(inscricao);
        }

        // GET: Inscricao/Create
        public IActionResult Create()
        {
            ViewData["InscritoID"] = new SelectList(_context.Inscrito, "InscritoID", "InscritoID");
            ViewData["LiveID"] = new SelectList(_context.Live, "LiveID", "LiveID");

            var liveNome = _context.Live.Select(l => new
            {
                LiveID = l.LiveID,
                LiveNome = l.Nome
            }).ToList();

            var inscritoNome = _context.Inscrito.Select(i => new
            {
                InscritoID = i.InscritoID,
                InscritoNome = i.Nome
            }).ToList();

            ViewBag.LiveNome = new MultiSelectList(liveNome, "LiveID", "LiveNome");
            ViewBag.InscritoNome = new MultiSelectList(inscritoNome, "InscritoID", "InscritoNome");

            return View();
        }

        //GET: Inscricao/Create -> Ao selecionar a live, ele insere o valor da inscrição definido no cadastro de live.
        public async Task<decimal> GetValorInscricao(int liveID)
        {
            var valorInscricao = _context.Live
                .Where(l => l.LiveID == liveID)
                .Select(l => l.ValorInscricao)
                .FirstOrDefault();

            return valorInscricao;
        }

        // POST: Inscricao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscricaoID,LiveID,ValorInscricao,InscritoID,DataVencimento,StatusPagamento")] Inscricao inscricao)
        {

            if (ModelState.IsValid)
            {
                var valorInscricao = _context.Live
               .Where(l => l.LiveID == inscricao.LiveID)
               .Select(l => l.ValorInscricao)
               .FirstOrDefault();

                inscricao.ValorInscricao = valorInscricao;

                _context.Add(inscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InscritoID"] = new SelectList(_context.Inscrito, "InscritoID", "InscritoID", inscricao.InscritoID);
            ViewData["LiveID"] = new SelectList(_context.Live, "LiveID", "LiveID", inscricao.LiveID);
            return View(inscricao);
        }

        // GET: Inscricao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inscricoes == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
            }

            var inscritoNome = _context.Inscrito.Select(i => new
            {
                InscritoID = i.InscritoID,
                InscritoNome = i.Nome
            }).ToList();

            ViewBag.InscritoNome = new MultiSelectList(inscritoNome, "InscritoID", "InscritoNome");


            var liveNome = _context.Live.Select(l => new
            {
                LiveID = l.LiveID,
                LiveNome = l.Nome,
                LiveValorInscricao = l.ValorInscricao
            }).ToList();

            ViewBag.LiveNome = new MultiSelectList(liveNome, "LiveID", "LiveNome");

            ViewData["InscritoID"] = new SelectList(_context.Inscrito, "InscritoID", "InscritoID", inscricao.InscritoID);
            ViewData["LiveID"] = new SelectList(_context.Live, "LiveID", "LiveID", inscricao.LiveID);
            return View(inscricao);
        }

        // POST: Inscricao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscricaoID,LiveID,ValorInscricao,InscritoID,DataVencimento,StatusPagamento")] Inscricao inscricao)
        {

            if (id != inscricao.InscricaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obter o valor da inscrição com base no cadastro de live.
                    var liveValorInscricao = GetValorInscricao(inscricao.LiveID);

                    // Atribuir o valor obtido ao campo ValorInscricao
                    inscricao.ValorInscricao = liveValorInscricao.Result;

                    _context.Update(inscricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricaoExists(inscricao.InscricaoID))
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
            ViewData["InscritoID"] = new SelectList(_context.Inscrito, "InscritoID", "InscritoID", inscricao.InscritoID);
            ViewData["LiveID"] = new SelectList(_context.Live, "LiveID", "LiveID", inscricao.LiveID);
            return View(inscricao);
        }

        // GET: Inscricao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inscricoes == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes
                .Include(i => i.Inscrito)
                .Include(i => i.Live)
                .FirstOrDefaultAsync(m => m.InscricaoID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

        // POST: Inscricao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inscricoes == null)
            {
                return Problem("Entity set 'Contexto.Inscricoes'  is null.");
            }
            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao != null)
            {
                _context.Inscricoes.Remove(inscricao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoExists(int id)
        {
            return (_context.Inscricoes?.Any(e => e.InscricaoID == id)).GetValueOrDefault();
        }
    }
}
