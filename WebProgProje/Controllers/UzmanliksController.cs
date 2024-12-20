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
    public class UzmanliksController : Controller
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
        public UzmanliksController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Uzmanliks
        public async Task<IActionResult> Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            return View(await _context.Uzmanliklar.ToListAsync());
        }

        // GET: Uzmanliks/Details/5
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

            var uzmanlik = await _context.Uzmanliklar
                .Include(u => u.IslemUzmanliklar)
                .ThenInclude(iu => iu.Islem)
                .FirstOrDefaultAsync(m => m.UzmanlikId == id);
            if (uzmanlik == null)
            {
                return NotFound();
            }

            return View(uzmanlik);
        }
        // GET: Uzmanliks/Create
        public IActionResult Create()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            ViewData["Islemler"] = new MultiSelectList(_context.Islemler, "IslemId", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UzmanlikId,Ad,SelectedIslemler")] Uzmanlik uzmanlik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uzmanlik);
                await _context.SaveChangesAsync();

                foreach (var islemId in uzmanlik.SelectedIslemler)
                {
                    _context.IslemUzmanliklar.Add(new IslemUzmanlik { UzmanlikId = uzmanlik.UzmanlikId, IslemId = islemId });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Islemler"] = new MultiSelectList(_context.Islemler, "IslemId", "Ad", uzmanlik.SelectedIslemler);
            return View(uzmanlik);
        }

        // GET: Uzmanliks/Edit/5
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

            var uzmanlik = await _context.Uzmanliklar
                .Include(u => u.IslemUzmanliklar)
                .FirstOrDefaultAsync(m => m.UzmanlikId == id);
            if (uzmanlik == null)
            {
                return NotFound();
            }

            uzmanlik.SelectedIslemler = uzmanlik.IslemUzmanliklar.Select(iu => iu.IslemId).ToArray();
            ViewData["Islemler"] = new MultiSelectList(_context.Islemler, "IslemId", "Ad", uzmanlik.SelectedIslemler);
            return View(uzmanlik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UzmanlikId,Ad,SelectedIslemler")] Uzmanlik uzmanlik)
        {
            if (id != uzmanlik.UzmanlikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uzmanlik);
                    await _context.SaveChangesAsync();

                    var existingIslemUzmanliklar = _context.IslemUzmanliklar.Where(iu => iu.UzmanlikId == id);
                    _context.IslemUzmanliklar.RemoveRange(existingIslemUzmanliklar);
                    await _context.SaveChangesAsync();

                    foreach (var islemId in uzmanlik.SelectedIslemler)
                    {
                        _context.IslemUzmanliklar.Add(new IslemUzmanlik { UzmanlikId = uzmanlik.UzmanlikId, IslemId = islemId });
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzmanlikExists(uzmanlik.UzmanlikId))
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
            ViewData["Islemler"] = new MultiSelectList(_context.Islemler, "IslemId", "Ad", uzmanlik.SelectedIslemler);
            return View(uzmanlik);
        }


        // GET: Uzmanliks/Delete/5
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

            var uzmanlik = await _context.Uzmanliklar
                .FirstOrDefaultAsync(m => m.UzmanlikId == id);
            if (uzmanlik == null)
            {
                return NotFound();
            }

            return View(uzmanlik);
        }

        // POST: Uzmanliks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uzmanlik = await _context.Uzmanliklar.FindAsync(id);
            if (uzmanlik != null)
            {
                _context.Uzmanliklar.Remove(uzmanlik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzmanlikExists(int id)
        {
            return _context.Uzmanliklar.Any(e => e.UzmanlikId == id);
        }
    }
}
