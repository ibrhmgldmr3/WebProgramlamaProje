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
    public class RandevusController : Controller
    {
        private readonly SalonDbContext _context;

        public RandevusController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Randevus
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Calisan")
            {
                return Unauthorized();
            }
            var salonDbContext = _context.Randevular.Include(r => r.Calisan).Include(r => r.Islem).Include(r => r.Kullanici).Include(r => r.Salon);
            return View(await salonDbContext.ToListAsync());
        }

        // GET: Randevus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Calisan")
            {
                return Unauthorized();
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

            return View(randevu);
        }

        // GET: Randevus/Create
        public IActionResult Create()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Calisan")
            {
                return Unauthorized();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad");
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad");
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName");
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim");
            return View();
        }

        // POST: Randevus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandevuId,CalisanId,IslemId,SalonId,KullaniciId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Ad", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islemler, "IslemId", "Ad", randevu.IslemId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", randevu.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", randevu.SalonId);
            return View(randevu);
        }

        // GET: Randevus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Calisan")
            {
                return Unauthorized();
            }
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", randevu.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", randevu.SalonId);
            return View(randevu);
        }

        // POST: Randevus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuId,CalisanId,IslemId,SalonId,KullaniciId,Tarih,Saat,OnaylandiMi")] Randevu randevu)
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", randevu.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", randevu.SalonId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Calisan")
            {
                return Unauthorized();
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
    }
}



