using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebProgramlamaProje.Controllers
{
    public class HairAPIController : Controller
    {
        private readonly ILogger<HairAPIController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HairAPIController(
            ILogger<HairAPIController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult ChangeHairstyleForm()
        {
            return View();
        }

        public IActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeHairstyle([FromBody] ChangeHairstyleRequest request)
        {
            if (request is null || string.IsNullOrEmpty(request.Base64Image))
            {
                return Json(new { error = "Lütfen bir resim yükleyin." });
            }

            // API bilgileri kaynak kodda tutulmaz; konfigürasyondan (User Secrets / ortam
            // değişkeni / appsettings) okunur. Bkz. README > Yapılandırma.
            var apiKey = _configuration["HairstyleApi:ApiKey"];
            var apiHost = _configuration["HairstyleApi:Host"];
            var endpoint = _configuration["HairstyleApi:Endpoint"];

            if (string.IsNullOrWhiteSpace(apiKey) ||
                string.IsNullOrWhiteSpace(apiHost) ||
                string.IsNullOrWhiteSpace(endpoint))
            {
                _logger.LogError("HairstyleApi yapılandırması eksik: ApiKey/Host/Endpoint tanımlı değil.");
                return Json(new { error = "Servis yapılandırması eksik. Lütfen yöneticinize başvurun." });
            }

            try
            {
                var imageBytes = Convert.FromBase64String(request.Base64Image);

                var client = _httpClientFactory.CreateClient();

                using var content = new MultipartFormDataContent();

                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "image_target",
                    FileName = "uploaded_image.jpg"
                };
                content.Add(imageContent);

                var hairTypeContent = new StringContent(request.HairType ?? string.Empty);
                hairTypeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "hair_type"
                };
                content.Add(hairTypeContent);

                using var apiRequest = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = content
                };
                apiRequest.Headers.Add("x-rapidapi-key", apiKey);
                apiRequest.Headers.Add("x-rapidapi-host", apiHost);

                using var response = await client.SendAsync(apiRequest);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // Yanıt gövdesi log'a yazılır, kullanıcıya sızdırılmaz.
                    _logger.LogError("Saç modeli servisi hata döndürdü. Durum kodu: {StatusCode}", response.StatusCode);
                    return Json(new { error = $"API Hatası: {response.StatusCode}" });
                }

                using var document = JsonDocument.Parse(responseBody);
                if (document.RootElement.TryGetProperty("data", out var data) &&
                    data.TryGetProperty("image", out var image))
                {
                    return Json(new { resultImage = image.GetString() });
                }

                _logger.LogWarning("Saç modeli servisinden beklenen alanlar dönmedi.");
                return Json(new { error = "Servisten beklenen yanıt alınamadı." });
            }
            catch (FormatException)
            {
                return Json(new { error = "Görsel formatı geçersiz. Lütfen tekrar deneyin." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Saç modeli değiştirme isteği başarısız oldu.");
                return Json(new { error = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." });
            }
        }

        public class ChangeHairstyleRequest
        {
            public string Base64Image { get; set; } = string.Empty;
            public string HairType { get; set; } = string.Empty;
        }
    }
}
