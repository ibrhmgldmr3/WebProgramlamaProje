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

        // Kullan�c�n�n rol�n� oturumdan almak
        private string GetUserRole()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == userEmail);
                return user?.Role; // Kullan�c� rol� yoksa null d�ner
            }
            return null;
        }
        public IActionResult YetkisizDeneme(string sayfaAdi) 
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Eri�im";
                return RedirectToAction("Index", "Home"); // Ana sayfaya y�nlendir
            }
            return View(sayfaAdi);
        }
        // Y�netici Paneli (Yaln�zca Admin kullan�c�lar� eri�ebilir)
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

        // Gizlilik Politikas�
        public IActionResult Privacy()
        {
            return View();
        }

        // �leti�im Sayfas�
        public IActionResult Iletisim()
        {
            return View();
        }

        // Hizmetlerimiz Sayfas�
        public IActionResult Hizmetlerimiz()
        {
            return View();
        }

        // Sa� Stili De�i�tirici (GET - Formu G�r�nt�ler)
       

    }
}
