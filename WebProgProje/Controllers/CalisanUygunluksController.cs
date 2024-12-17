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
    public class CalisanUygunluksController : Controller
    {
        private readonly SalonDbContext _context;

        public CalisanUygunluksController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: CalisanUygunluks
        public async Task<IActionResult> Index()
        {
            var salonDbContext = _context.CalisanUygunluklar.Include(c => c.Calisan);
            return View(await salonDbContext.ToListAsync());
        }

        // GET: CalisanUygunluks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanUygunluk = await _context.CalisanUygunluklar
                .Include(c => c.Calisan)
                .FirstOrDefaultAsync(m => m.CalisanUygunlukId == id);
            if (calisanUygunluk == null)
            {
                return NotFound();
            }

            return View(calisanUygunluk);
        }

        // GET: CalisanUygunluks/Create
        public IActionResult Create()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad");
            return View();
        }

        // POST: CalisanUygunluks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalisanUygunlukId,CalisanId,Gun,Baslangic,Bitis")] CalisanUygunluk calisanUygunluk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calisanUygunluk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", calisanUygunluk.CalisanId);
            return View(calisanUygunluk);
        }

        // GET: CalisanUygunluks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanUygunluk = await _context.CalisanUygunluklar.FindAsync(id);
            if (calisanUygunluk == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", calisanUygunluk.CalisanId);
            return View(calisanUygunluk);
        }

        // POST: CalisanUygunluks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalisanUygunlukId,CalisanId,Gun,Baslangic,Bitis")] CalisanUygunluk calisanUygunluk)
        {
            if (id != calisanUygunluk.CalisanUygunlukId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisanUygunluk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanUygunlukExists(calisanUygunluk.CalisanUygunlukId))
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", calisanUygunluk.CalisanId);
            return View(calisanUygunluk);
        }

        // GET: CalisanUygunluks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanUygunluk = await _context.CalisanUygunluklar
                .Include(c => c.Calisan)
                .FirstOrDefaultAsync(m => m.CalisanUygunlukId == id);
            if (calisanUygunluk == null)
            {
                return NotFound();
            }

            return View(calisanUygunluk);
        }

        // POST: CalisanUygunluks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisanUygunluk = await _context.CalisanUygunluklar.FindAsync(id);
            if (calisanUygunluk != null)
            {
                _context.CalisanUygunluklar.Remove(calisanUygunluk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanUygunlukExists(int id)
        {
            return _context.CalisanUygunluklar.Any(e => e.CalisanUygunlukId == id);
        }
    }
}
