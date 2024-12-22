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

        // Y�netici Paneli (Yaln�zca Admin kullan�c�lar� eri�ebilir)
        public IActionResult Admin()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized(); // Yetkisiz eri�im
            }
            return View();
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
        public IActionResult ChangeHairstyleForm()
        {
            return View();
        }

        // Sa� Stili De�i�tirici (POST - Kullan�c� Resmini API'ye G�nderir)
        [HttpPost]
        public async Task<IActionResult> ChangeHairstyle(IFormFile image, string hairType)
        {
            if (image == null || image.Length == 0)
            {
                ViewBag.Message = "L�tfen bir resim y�kleyin.";
                return View("ChangeHairstyleForm");
            }

            try
            {
                // Resmi ge�ici dosyaya kaydetme
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // API �ste�i
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle"),
                    Headers =
            {
                { "x-rapidapi-key", "f1283a93f9msh05b5b5e4a6c3cf1p130cb6jsnf53ff6c77299" },
                { "x-rapidapi-host", "hairstyle-changer.p.rapidapi.com" }
            },
                    Content = new MultipartFormDataContent
            {
                new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(filePath))
                {
                    Headers =
                    {
                        ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "image_target",
                            FileName = image.FileName
                        }
                    }
                },
                new StringContent(hairType)
                {
                    Headers =
                    {
                        ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "hair_type"
                        }
                    }
                }
            }
                };

                using (var response = await client.SendAsync(request))
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                        ViewBag.ResultImage = jsonResponse.data.image; // Base64 string
                        return View("Result");
                    }
                    else
                    {
                        ViewBag.Error = $"API Hatas�: {response.StatusCode}";
                        return View("Result");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata olu�tu: {ex.Message}";
                return View("Result");
            }
        }

    }
}
