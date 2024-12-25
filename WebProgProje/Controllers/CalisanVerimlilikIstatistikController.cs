using Microsoft.AspNetCore.Mvc;
namespace WebProgramlamaProje.Controllers
{
    public class CalisanVerimlilikController : Controller
    {
        private readonly HttpClient _httpClient;

        public CalisanVerimlilikController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Verimlilik()
        {
            var verimlilikList = await _httpClient.GetFromJsonAsync<List<CalisanVerimlilik>>("http://localhost:5237/api/CalisanVerimlilikIstatistik/verimlilik");
            return View(verimlilikList);
        }

        public async Task<IActionResult> GunlukKazanc(DateTime tarih)
        {
            var gunlukKazancList = await _httpClient.GetFromJsonAsync<List<GunlukKazanclar>>($"http://localhost:5237/api/CalisanVerimlilikIstatistik/gunluk-kazanc?tarih={tarih:yyyy-MM-dd}");
            return View(gunlukKazancList);
        }

        public async Task<IActionResult> SalonIslemIstatistikleri()
        {
            var istatistikler = await _httpClient.GetFromJsonAsync<List<IslemIstatistik>>("http://localhost:5237/api/CalisanVerimlilikIstatistik/salon/islem-istatistikleri");
            return View(istatistikler);
        }

        public async Task<IActionResult> RandevuIstatistikleri(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var istatistikler = await _httpClient.GetFromJsonAsync<List<RandevuIstatistik>>($"http://localhost:5237/api/CalisanVerimlilikIstatistik/randevu-istatistikleri?baslangicTarihi={baslangicTarihi:yyyy-MM-dd}&bitisTarihi={bitisTarihi:yyyy-MM-dd}");
            return View(istatistikler);
        }

        public async Task<IActionResult> CalismaSaatleri()
        {
            var calismaSaatleri = await _httpClient.GetFromJsonAsync<List<CalismaSaati>>("http://localhost:5237/api/CalisanVerimlilikIstatistik/calisan/calisma-saatleri");
            return View(calismaSaatleri);
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