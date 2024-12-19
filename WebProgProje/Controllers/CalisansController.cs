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
    public class CalisansController : Controller
    {
        private readonly SalonDbContext _context;

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

        public CalisansController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Calisans
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            var salonDbContext = _context.Calisanlar.Include(c => c.Kullanici).Include(c => c.Salon).Include(c => c.Uzmanlik);
            return View(await salonDbContext.ToListAsync());
        }

        // GET: Calisans/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var calisan = await _context.Calisanlar
                .Include(c => c.Kullanici)
                .Include(c => c.Salon)
                .Include(c => c.Uzmanlik)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // GET: Calisans/Create
        public IActionResult Create()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad");
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim");
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName");
            return View();
        }

        // POST: Calisans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalisanId,KullaniciId,Ad,Soyad,UzmanlikId,SalonId")] Calisan calisan)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(calisan.KullaniciId);
            if (kullanici != null)
            {
                var fullNameParts = kullanici.FullName.Split(' ');
                calisan.Ad = fullNameParts[0];
                calisan.Soyad = fullNameParts.Length > 1 ? string.Join(' ', fullNameParts.Skip(1)) : string.Empty;

            }


            _context.Add(calisan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad", calisan.UzmanlikId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", calisan.SalonId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", calisan.KullaniciId);
            return View(calisan);
        }

        // GET: Calisans/Edit/5
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

            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad", calisan.UzmanlikId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", calisan.SalonId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", calisan.KullaniciId);
            return View(calisan);
        }

        // POST: Calisans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalisanId,KullaniciId,Ad,Soyad,UzmanlikId,SalonId,CalismaSaatiGiris,CalismaSaatiCikis")] Calisan calisan)
        {
            if (id != calisan.CalisanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = await _context.Kullanicilar.FindAsync(calisan.KullaniciId);
                    if (kullanici != null)
                    {
                        calisan.Ad = kullanici.FullName;
                    }
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanExists(calisan.CalisanId))
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
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad", calisan.UzmanlikId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Isim", calisan.SalonId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "FullName", calisan.KullaniciId);
            return View(calisan);
        }

        // GET: Calisans/Delete/5
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

            var calisan = await _context.Calisanlar
                .Include(c => c.Kullanici)
                .Include(c => c.Salon)
                .Include(c => c.Uzmanlik)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // POST: Calisans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan != null)
            {
                _context.Calisanlar.Remove(calisan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanExists(int id)
        {
            return _context.Calisanlar.Any(e => e.CalisanId == id);
        }
    }
}




