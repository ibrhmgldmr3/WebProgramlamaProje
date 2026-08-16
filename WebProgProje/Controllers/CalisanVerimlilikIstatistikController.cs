using Microsoft.AspNetCore.Mvc;
using WebProgramlamaProje.Models;
using System.Net.Http.Json;

namespace WebProgramlamaProje.Controllers
{
    public class CalisanVerimlilikController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CalisanVerimlilikController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            // API adresi kaynak kodda sabit tutulmaz; konfigurasyondan okunur.
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiBaseUrl = string.IsNullOrWhiteSpace(baseUrl)
                ? "http://localhost:5237"
                : baseUrl.TrimEnd('/');
        }

        private string GetUserRole()
        {
            return HttpContext.Session.GetString("UserRole");
        }

        private object yetkisizDeneme()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            return null;
        }

        public IActionResult Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            return View();
        }

        public async Task<IActionResult> Verimlilik()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            var verimlilikList = await _httpClient.GetFromJsonAsync<List<CalisanVerimlilik>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/verimlilik");
            return View(verimlilikList);
        }

        public async Task<IActionResult> GunlukKazanc(DateTime tarih)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            var gunlukKazancList = await _httpClient.GetFromJsonAsync<List<GunlukKazanclar>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/gunluk-kazanc?tarih={tarih:yyyy-MM-dd}");
            return View(gunlukKazancList);
        }

        public async Task<IActionResult> SalonIslemIstatistikleri()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            var istatistikler = await _httpClient.GetFromJsonAsync<List<IslemIstatistik>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/salon/islem-istatistikleri");
            return View(istatistikler);
        }

        public async Task<IActionResult> RandevuIstatistikleri(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                ViewData["ErrorMessage"] = "Yetkisiz Erişim";
                return RedirectToAction("Index", "home");
            }
            var istatistikler = await _httpClient.GetFromJsonAsync<List<RandevuIstatistik>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/randevu-istatistikleri?baslangicTarihi={baslangicTarihi:yyyy-MM-dd}&bitisTarihi={bitisTarihi:yyyy-MM-dd}");
            return View(istatistikler);
        }

        public async Task<IActionResult> CalismaSaatleri()
        {
            var calismaSaatleri = await _httpClient.GetFromJsonAsync<List<CalismaSaati>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/calisan/calisma-saatleri");
            return View(calismaSaatleri);
        }

        public async Task<IActionResult> KullaniciRandevular(int kullaniciId)
        {
            try
            {
                var randevular = await _httpClient.GetFromJsonAsync<List<Randevu>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/kullanici/{kullaniciId}/randevular");
                if (randevular == null || !randevular.Any())
                {
                    ViewData["ErrorMessage"] = "Bu kullanıcıya ait randevu bulunamadı.";
                    return View(new List<Randevu>());
                }
                return View(randevular);
            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Randevular yüklenirken bir hata oluştu: " + ex.Message;
                return View(new List<Randevu>());
            }
        }

        [HttpGet("api/randevus/calisan/{calisanId}/uygunluk")]
        public async Task<IActionResult> GetCalisanUygunluk(int calisanId)
        {
            var uygunluklar = await _httpClient.GetFromJsonAsync<List<CalismaSaati>>($"{_apiBaseUrl}/api/CalisanVerimlilikIstatistik/calisan/{calisanId}/uygunluk");
            if (uygunluklar == null || !uygunluklar.Any())
            {
                return NotFound(new { Message = "Bu çalışana ait uygunluk bilgisi bulunamadı." });
            }
            return Ok(uygunluklar);
        }

        public class CalisanVerimlilik
        {
            public int CalisanId { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public int RandevuSayisi { get; set; }
            public decimal ToplamKazanc { get; set; }
        }

        public class GunlukKazanclar
        {
            public int CalisanId { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public decimal GunlukKazancNet { get; set; }
        }

        public class IslemIstatistik
        {
            public string IslemAdi { get; set; }
            public int ToplamIslemSayisi { get; set; }
            public decimal ToplamKazanc { get; set; }
        }

        public class Randevu
        {
            public int RandevuId { get; set; }
            public DateTime Tarih { get; set; }
            public TimeSpan Saat { get; set; }
            public string IslemAdi { get; set; }
            public decimal IslemUcreti { get; set; }
            public string SalonAdi { get; set; }
            public string CalisanAdi { get; set; }
            public string CalisanSoyadi { get; set; }
            public bool OnaylandiMi { get; set; }
        }

        public class RandevuIstatistik
        {
            public DateTime Tarih { get; set; }
            public int ToplamRandevuSayisi { get; set; }
            public decimal ToplamKazanc { get; set; }
        }

        public class CalismaSaati
        {
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public DayOfWeek Gun { get; set; }
            public TimeSpan Baslangic { get; set; }
            public TimeSpan Bitis { get; set; }
        }
    }
}
