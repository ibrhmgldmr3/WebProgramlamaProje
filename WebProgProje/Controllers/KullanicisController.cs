using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
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
            return HttpContext.Session.GetString("UserRole") ?? string.Empty;
        }

        public IActionResult YetkisizDeneme(string sayfaAdi)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                TempData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
            }
            return View(sayfaAdi);
        }

        private async Task<int?> GetUserId()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail != null)
            {
                var user = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (user != null)
                {
                    return user.KullaniciId;
                }
            }
            return null;
        }

        // GET: Kullanicis
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();

            if (userRole != "Admin")
            {
                TempData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
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
            var userId = await GetUserId();
            if (userId != kullanici.KullaniciId)
            {
                TempData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
            }

            return View(kullanici);
        }

        // GET: Kullanicis/Create
        public IActionResult Kaydolma()
        {
            return View();
        }

        // POST: Kullanicis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kaydolma([Bind("Email,PasswordHash,FullName,PhoneNumber,ProfilResmi")] Kullanici kullanici)
        {
            kullanici.ProfilResmi = "";
            kullanici.Role = "Member";

            // Aynı e-posta adresiyle kayıtlı kullanıcı var mı kontrol et
            var existingUser = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Email == kullanici.Email);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Bu e-posta adresiyle daha önce kayıt yapılmış! >:(";
                return View(kullanici);
            }

            if (ModelState.IsValid)
            {
                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                TempData["Message"] = kullanici.Email + ' ' + kullanici.FullName + " kaydınız başarıyla tamamlandı!! :)";

                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Kaydolma Başarısız! ):";

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
            var userId = await GetUserId();
            if (userId != kullanici.KullaniciId)
            {
                TempData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
            }

            return View(kullanici);
        }

        // POST: Kullanicis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciId,Email,PasswordHash,Role,FullName,PhoneNumber,ProfilResmi")] Kullanici kullanici)
        {
            if (id != kullanici.KullaniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (kullanici.KullaniciId != id)
                    {
                        TempData["ErrorMessage"] = "Yetkisiz Erişim";
                        RedirectToAction("Index", "home");
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
            var userId = await GetUserId();
            if (userId != kullanici.KullaniciId)
            {
                TempData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
            }

            return View(kullanici);
        }

        // POST: Kullanicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                TempData["Message"] = "Silme işlemi başarısız! ):";
                return RedirectToAction("Delete", "Kullanicis");
            }

            TempData["Message"] = kullanici.Email + ' ' + kullanici.FullName + " silme işlemi başarıyla tamamlandı!!";
            _context.Kullanicilar.Remove(kullanici);
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
                    HttpContext.Session.SetString("UserId", user.KullaniciId.ToString());

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

        private Kullanici? AuthenticateUser(string email, string password)
        {
            return _context.Kullanicilar.SingleOrDefault(u => u.Email == email && u.PasswordHash == password);
        }

    }
}