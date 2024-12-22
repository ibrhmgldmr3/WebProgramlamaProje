using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace WebProgramlamaProje.Controllers
{
    public class HairAPIController : Controller
    {
        private readonly ILogger<HairAPIController> _logger;

        public HairAPIController(ILogger<HairAPIController> logger)
        {
            _logger = logger;
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
            if (string.IsNullOrEmpty(request.Base64Image))
            {
                return Json(new { error = "Lütfen bir resim yükleyin." });
            }

            try
            {
                var imageBytes = Convert.FromBase64String(request.Base64Image);

                var client = new HttpClient();
                var apiRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://www.ailabapi.com/api/portrait/effects/hairstyle-editor"),
                    Headers =
            {
                { "ailabapi-api-key", "xasCgd1zEDH8WbKP67pQ7o0We6OrcTIZSXt3XokLrymMcuDlfRqIh5T0Mwe1taEA" },
            },
                    Content = new MultipartFormDataContent
            {
                new ByteArrayContent(imageBytes)
                {
                    Headers =
                    {
                        ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "image_target",
                            FileName = "uploaded_image.jpg"
                        }
                    }
                },
                new StringContent(request.HairType)
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

                using (var response = await client.SendAsync(apiRequest))
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                        return Json(new { resultImage = jsonResponse?.data?.image?.ToString() });
                    }
                    else
                    {
                        return Json(new { error = $"API Hatası: {response.StatusCode}" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        public class ChangeHairstyleRequest
        {
            public string Base64Image { get; set; }
            public string HairType { get; set; }
        }

    }
}
