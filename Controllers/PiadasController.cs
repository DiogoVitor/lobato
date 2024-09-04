using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Piadas.Data;
using Piadas.Models;

namespace Piadas.Controllers
{
    public class PiadasController : Controller
    {
        private readonly PiadasdbContext _context;

        public PiadasController(PiadasdbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Busca()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(String TermoDeBusca)
        {
            List<piadas> Piadoca = await _context.piadas.Where(f => f.pergunta.Contains(TermoDeBusca)).ToListAsync();
            return View("Index", Piadoca);
        }

        // GET: Piadas
        public async Task<IActionResult> Index()
        {
            return View(await _context.piadas.ToListAsync());
        }

        // GET: Piadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piadas = await _context.piadas
                .FirstOrDefaultAsync(m => m.id == id);
            if (piadas == null)
            {
                return NotFound();
            }

            return View(piadas);
        }

        // GET: Piadas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Piadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,pergunta,Resposta")] piadas piadas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piadas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piadas);
        }

        // GET: Piadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piadas = await _context.piadas.FindAsync(id);
            if (piadas == null)
            {
                return NotFound();
            }
            return View(piadas);
        }

        // POST: Piadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,pergunta,Resposta")] piadas piadas)
        {
            if (id != piadas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piadas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!piadasExists(piadas.id))
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
            return View(piadas);
        }

        // GET: Piadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piadas = await _context.piadas
                .FirstOrDefaultAsync(m => m.id == id);
            if (piadas == null)
            {
                return NotFound();
            }

            return View(piadas);
        }

        // POST: Piadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piadas = await _context.piadas.FindAsync(id);
            if (piadas != null)
            {
                _context.piadas.Remove(piadas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool piadasExists(int id)
        {
            return _context.piadas.Any(e => e.id == id);
        }
    }
}
