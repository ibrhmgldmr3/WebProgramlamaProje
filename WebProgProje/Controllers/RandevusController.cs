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

        public RandevusController(SalonDbContext context)
        {
            _context = context;
        }
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
        // GET: Randevus

        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            var userId = GetUserId();

            IQueryable<Randevu> randevular = _context.Randevular
                .Include(r => r.Calisan)
                .ThenInclude(c => c.Uzmanlik) // Uzmanlık ilişkisini dahil ediyoruz
                .Include(r => r.Islem)
                .Include(r => r.Kullanici)
                .Include(r => r.Salon);

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
                .Include(r => r.Salon)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevus/Create
        public IActionResult RandevuAl()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Kullanicis");
            }

            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad");
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim");
            ViewData["KullaniciId"] = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RandevuAl([Bind("RandevuId,CalisanId,IslemId,SalonId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
        {
            randevu.KullaniciId = GetUserId(); // Kullanıcı ID'sini doğrudan session'dan alarak ayarlayın

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
            else
            {
                var islem = await _context.Islemler.FindAsync(randevu.IslemId);
                if (islem != null)
                {
                    var randevuBaslangic = randevu.Saat;
                    var randevuBitis = randevu.Saat.Add(islem.Sure);

                    if (randevuBaslangic < calisanUygunluk.Baslangic || randevuBitis > calisanUygunluk.Bitis)
                    {
                        ModelState.AddModelError("Saat", "Randevu saati çalışanın mesai saatleri dışında.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", randevu.SalonId);
            return View(randevu);
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

        // GET: Randevus/GetCalisanlarByIslem
        public JsonResult GetCalisanlarByIslem(int islemId)
        {
            var calisanlar = _context.Calisanlar
                .Where(c => c.Uzmanlik.IslemUzmanliklar.Any(iu => iu.IslemId == islemId))
                .Select(c => new { c.CalisanId, c.Ad })
                .ToList();
            return Json(calisanlar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuId,CalisanId,IslemId,SalonId,KullaniciId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
        {
            if (id != randevu.RandevuId)
            {
                return NotFound();
            }

            var userRole = GetUserRole();
            if (userRole != "Employee" && userRole != "Admin")
            {
                var originalRandevu = await _context.Randevular.AsNoTracking().FirstOrDefaultAsync(r => r.RandevuId == id);
                if (originalRandevu != null && randevu.OnaylandiMi != originalRandevu.OnaylandiMi)
                {
                    ModelState.AddModelError("OnaylandiMi", "Randevunun onaylanma durumunu sadece çalışanlar ve yöneticiler değiştirebilir.");
                }
            }

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
            else
            {
                var islem = await _context.Islemler.FindAsync(randevu.IslemId);
                if (islem != null)
                {
                    var randevuBaslangic = randevu.Saat;
                    var randevuBitis = randevu.Saat.Add(islem.Sure);

                    if (randevuBaslangic < calisanUygunluk.Baslangic || randevuBitis > calisanUygunluk.Bitis)
                    {
                        ModelState.AddModelError("Saat", "Randevu saati çalışanın mesai saatleri dışında.");
                    }
                }
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar.Include(c => c.Kullanici), "CalisanId", "Kullanici.FullName", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", randevu.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Adres", randevu.SalonId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                RedirectToAction("Index","Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Kullanici)
                .Include(r => r.Salon)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            if (randevu.OnaylandiMi)
            {
                TempData["ErrorMessage"] = "Randevunuz çoktan onaylanmıştır, silemezsiniz.";
                return RedirectToAction(nameof(Index));
            }

            return View(randevu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                if (randevu.OnaylandiMi)
                {
                    TempData["ErrorMessage"] = "Randevunuz çoktan onaylanmıştır, silemezsiniz.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Randevular.Remove(randevu);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.RandevuId == id);
        }
    }
}
