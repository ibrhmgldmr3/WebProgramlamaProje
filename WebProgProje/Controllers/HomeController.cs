using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalonDbContext _context;

        public HomeController(SalonDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Kullanýcýnýn rolünü oturumdan almak
        private string GetUserRole()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == userEmail);
                return user?.Role; // Kullanýcý rolü yoksa null döner
            }
            return null;
        }
        public IActionResult YetkisizDeneme(string sayfaAdi) 
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Eriþim";
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
            }
            return View(sayfaAdi);
        }
        // Yönetici Paneli (Yalnýzca Admin kullanýcýlarý eriþebilir)
        public IActionResult Admin()
        {
            return YetkisizDeneme("Admin");
        }

        // Ana Sayfa
        public IActionResult Index()
        {
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserFullName = HttpContext.Session.GetString("UserFullName");
            return View();
        }

        // Gizlilik Politikasý
        public IActionResult Privacy()
        {
            return View();
        }

        // Ýletiþim Sayfasý
        public IActionResult Iletisim()
        {
            return View();
        }

        // Hizmetlerimiz Sayfasý
        public IActionResult Hizmetlerimiz()
        {
            return View();
        }

        // Saç Stili Deðiþtirici (GET - Formu Görüntüler)
       

    }
}
