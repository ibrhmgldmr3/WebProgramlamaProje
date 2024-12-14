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
    public class KullanicisController : Controller
    {
        private readonly SalonDbContext _context;

        public KullanicisController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Kullanicis
        public async Task<IActionResult> Index()
        {
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
        public IActionResult Create()
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PasswordHash,FullName,PhoneNumber")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                kullanici.Role = "Müşteri"; // Role alanını koda gömülü olarak ayarlayın
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciId,Email,PasswordHash,Role,FullName,PhoneNumber")] Kullanici kullanici)
        {
            if (id != kullanici.KullaniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(model.Email, model.Password);
                if (user != null)
                {
                    // Kullanıcı bilgilerini session'a kaydet
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserFullName", user.FullName);

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

        private Kullanici AuthenticateUser(string email, string password)
        {
            // Bu metot, kullanıcıyı doğrulamak için veritabanı kontrolü yapar
            var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == email && u.PasswordHash == password);
            return user;
        }
    }
}

