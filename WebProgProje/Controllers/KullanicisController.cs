using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgProje.Controllers
{
    public class KullanicisController : Controller
    {
        private readonly SalonDbContext _context;

        public KullanicisController(SalonDbContext context)
        {
            _context = context;
        }

        private string GetUserRole()
        {
            return HttpContext.Session.GetString("UserRole");
        }

        // GET: Kullanicis
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            return View(await _context.Kullanicilar.ToListAsync());
        }

        // GET: Kullanicis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciId == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // GET: Kullanicis/Create
        public IActionResult Kaydolma()
        {
            return View();
        }

        public byte[] ConvertImageToByteArray(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // POST: Kullanicis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kaydolma([Bind("Email,PasswordHash,FullName,PhoneNumber")] Kullanici kullanici, IFormFile profilResmi)
        {
            kullanici.Role = "Member";
            if (ModelState.IsValid)
            {
                if (profilResmi != null)
                {
                    kullanici.ProfilResmi = ConvertImageToByteArray(profilResmi);
                }

                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                TempData["Message"] = kullanici.Email + ' ' + kullanici.FullName + " kaydınız başarıyla tamamlandı!!";

                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Kaydolma Başarısız! ):";

            return View(kullanici);
        }



        // GET: Kullanicis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanicis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciId,Email,PasswordHash,Role,FullName,PhoneNumber,ProfilResmi")] Kullanici kullanici, IFormFile profilResmi)
        {
            if (id != kullanici.KullaniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (profilResmi != null)
                    {
                        kullanici.ProfilResmi = ConvertImageToByteArray(profilResmi);
                    }
                    _context.Update(kullanici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullaniciExists(kullanici.KullaniciId))
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
            return View(kullanici);
        }

        // GET: Kullanicis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciId == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // POST: Kullanicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullanicilar.Remove(kullanici);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KullaniciExists(int id)
        {
            return _context.Kullanicilar.Any(e => e.KullaniciId == id);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(model.Email, model.Password);
                if (user != null && user.Role != null)
                {
                    // Kullanıcı bilgilerini session'a kaydet
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);

                    // Ana sayfaya yönlendir
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            // Session bilgilerini sıfırla
            HttpContext.Session.Clear();

            // Home/Index sayfasına yönlendir
            return RedirectToAction("Index", "Home");
        }

        private Kullanici AuthenticateUser(string email, string password)
        {
            // Bu metot, kullanıcıyı doğrulamak için veritabanı kontrolü yapar
            return _context.Kullanicilar.SingleOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
    }
}




