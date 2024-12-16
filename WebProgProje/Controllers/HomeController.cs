using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgProje.Controllers
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
        public IActionResult Admin()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Unauthorized();
            }
            return View();
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userFullName = HttpContext.Session.GetString("UserFullName");

            ViewBag.UserEmail = userEmail;
            ViewBag.UserFullName = userFullName;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Iletisim()
        {
            return View();
        }

        public IActionResult Hizmetlerimiz()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
