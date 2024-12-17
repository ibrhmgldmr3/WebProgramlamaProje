using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgProje.Controllers
{
    public class IslemsController : Controller
    {
        private readonly SalonDbContext _context;
  

        public IslemsController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Islems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Islemler.ToListAsync());
        }

        // GET: Islems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler
                .FirstOrDefaultAsync(m => m.IslemId == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }
        private string GetUserRole()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail != null)
            {
                var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    return user.Role;
                }
            }
            return null;
        }
        // GET: Islems/Create
        public IActionResult Create()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad");  // Uzmanlıkları listele
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IslemId,Ad,Sure,Ucret,Uzmanliklar")] Islem islem, int[] Uzmanliklar)
        {
            if (ModelState.IsValid)
            {
                foreach (var uzmanlikId in Uzmanliklar)
                {
                    var uzmanlik = await _context.Uzmanliklar.FindAsync(uzmanlikId);
                    islem.Uzmanliklar.Add(uzmanlik);  // İşlemle ilişkilendirilen uzmanlıkları ekle
                }
                _context.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(islem);
        }


        public IActionResult Hizmetlerimiz()
        {
            return View();
        }

        // GET: Islems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }
            return View(islem);
        }

        // POST: Islems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IslemId,Ad,Sure,Ucret")] Islem islem)
        {
            if (id != islem.IslemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemExists(islem.IslemId))
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
            return View(islem);
        }

        // GET: Islems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler
                .FirstOrDefaultAsync(m => m.IslemId == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // POST: Islems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem != null)
            {
                _context.Islemler.Remove(islem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslemExists(int id)
        {
            return _context.Islemler.Any(e => e.IslemId == id);
        }
    }
}

