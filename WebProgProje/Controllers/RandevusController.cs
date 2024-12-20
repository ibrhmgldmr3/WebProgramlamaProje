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
    public class RandevusController : Controller
    {
        private readonly SalonDbContext _context;
        private string? GetUserRole()
        {
            if (HttpContext.Session == null || !HttpContext.Session.IsAvailable)
            {
                return null;
            }
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
        public JsonResult GetCalisanUygunluk(int calisanId)
        {
            var uygunluklar = _context.CalisanUygunluklar
                .Where(cu => cu.CalisanId == calisanId)
                .Select(cu => new
                {
                    cu.Gun,
                    cu.Baslangic,
                    cu.Bitis
                }).ToList();

            return Json(uygunluklar);
        }
        private int? GetUserId()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail != null)
            {
                var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    return user.KullaniciId;
                }
            }
            return null;
        }
        public RandevusController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Randevus
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            var userId = GetUserId();

            IQueryable<Randevu> randevular = _context.Randevular.Include(r => r.Calisan).Include(r => r.Islem).Include(r => r.Kullanici);

            if (userRole == "Admin")
            {
                // Admin tüm randevuları görebilir
                randevular = randevular;
            }
            if (userRole == "Member")
            {
                // Müşteri sadece kendi randevularını görebilir
                randevular = randevular.Where(r => r.KullaniciId == userId);
            }
            if (userRole == "Employee")
            {
                // Çalışan sadece kendi randevularını görebilir
                randevular = randevular.Where(r => r.CalisanId == userId);
            }

            return View(await randevular.ToListAsync());
        }


        // GET: Randevus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Kullanici)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevus/Create
        // GET: Randevus/Create
        public IActionResult RandevuAl()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad");
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad");
            return View();
        }

        // POST: Randevus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RandevuAl([Bind("RandevuId,CalisanId,IslemId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
        {
            if (randevu.Tarih < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("Tarih", "Randevu tarihi bugünden önce olamaz.");
            }

            var calisanUygunluk = await _context.CalisanUygunluklar
                .Where(cu => cu.CalisanId == randevu.CalisanId && cu.Gun == randevu.Tarih.DayOfWeek)
                .FirstOrDefaultAsync();

            if (calisanUygunluk == null)
            {
                ModelState.AddModelError("CalisanId", "Seçilen çalışan bu gün çalışmıyor.");
            }
            else if (randevu.Saat < calisanUygunluk.Baslangic || randevu.Saat > calisanUygunluk.Bitis)
            {
                ModelState.AddModelError("Saat", "Seçilen saat çalışanın çalışma saatleri dışında.");
            }
            else
            {
                var calisanRandevular = await _context.Randevular
                    .Where(r => r.CalisanId == randevu.CalisanId && r.Tarih == randevu.Tarih)
                    .ToListAsync();

                foreach (var mevcutRandevu in calisanRandevular)
                {
                    var islemSuresi = await _context.Islemler
                        .Where(i => i.IslemId == mevcutRandevu.IslemId)
                        .Select(i => i.Sure)
                        .FirstOrDefaultAsync();

                    if (mevcutRandevu.Saat <= randevu.Saat && randevu.Saat < mevcutRandevu.Saat.Add(islemSuresi) && mevcutRandevu.Tarih == randevu.Tarih)
                    {
                        ModelState.AddModelError("Saat", "Seçilen saatte çalışan başka bir işlem yapıyor.");
                        break;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                randevu.KullaniciId = GetUserId().Value;
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            return View(randevu);
        }

        // GET: Randevus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", randevu.KullaniciId);
            return View(randevu);
        }

        // POST: Randevus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuId,CalisanId,IslemId,KullaniciId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
        {
            if (id != randevu.RandevuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.RandevuId))
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", randevu.KullaniciId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Kullanici)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.RandevuId == id);
        }

    }
}