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
            var salonDbContext = _context.Calisanlar.Include(c => c.Kullanici).Include(c => c.Salon);
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
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var calisan = await _context.Calisanlar
                    .Include(c => c.Kullanici)
                    .Include(c => c.Salon)
                    .FirstOrDefaultAsync(m => m.CalisanId == id);
                if (calisan == null)
                {
                    return NotFound();
                }

                return View(calisan);
            }
        }

        // GET: Calisans/Create
        public async Task<IActionResult> Create()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad");  // Uzmanlıkları listele
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Adres");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalisanId,KullaniciId,Ad,Soyad,UzmanlikId")] Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                calisan.Uzmanlik = await _context.Uzmanliklar.FindAsync(calisan.UzmanlikId);  // Uzmanlık bilgilerini al
                calisan.Salon = await _context.Salonlar.FindAsync(calisan.SalonId);
                _context.Add(calisan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzmanlikId"] = new SelectList(_context.Uzmanliklar, "UzmanlikId", "Ad", calisan.UzmanlikId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Adres", calisan.SalonId);
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", calisan.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Adres", calisan.SalonId);
            return View(calisan);
        }

        // POST: Calisans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalisanId,KullaniciId,Ad,Soyad,Uzmanlik,SalonId")] Calisan calisan)
        {
            if (id != calisan.CalisanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", calisan.KullaniciId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "Adres", calisan.SalonId);
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
