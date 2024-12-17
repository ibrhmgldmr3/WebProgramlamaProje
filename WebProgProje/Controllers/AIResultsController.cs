using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class AIResultsController : Controller
    {
        private readonly SalonDbContext _context;

        public AIResultsController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: AIResults
        public async Task<IActionResult> Index()
        {
            var salonDbContext = _context.AIResults.Include(a => a.Kullanici);
            return View(await salonDbContext.ToListAsync());
        }

        // GET: AIResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.AIResultId == id);
            if (aIResult == null)
            {
                return NotFound();
            }

            return View(aIResult);
        }

        // GET: AIResults/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email");
            return View();
        }

        // POST: AIResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AIResultId,KullaniciId,SuggestedColor,SuggestedStyle,CreatedAt")] AIResult aIResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aIResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", aIResult.KullaniciId);
            return View(aIResult);
        }

        // GET: AIResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults.FindAsync(id);
            if (aIResult == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", aIResult.KullaniciId);
            return View(aIResult);
        }

        // POST: AIResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AIResultId,KullaniciId,SuggestedColor,SuggestedStyle,CreatedAt")] AIResult aIResult)
        {
            if (id != aIResult.AIResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aIResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AIResultExists(aIResult.AIResultId))
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", aIResult.KullaniciId);
            return View(aIResult);
        }

        // GET: AIResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.AIResultId == id);
            if (aIResult == null)
            {
                return NotFound();
            }

            return View(aIResult);
        }

        // POST: AIResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aIResult = await _context.AIResults.FindAsync(id);
            if (aIResult != null)
            {
                _context.AIResults.Remove(aIResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AIResultExists(int id)
        {
            return _context.AIResults.Any(e => e.AIResultId == id);
        }
    }
}
