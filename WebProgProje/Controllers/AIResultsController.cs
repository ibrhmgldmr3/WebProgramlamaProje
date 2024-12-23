using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class AIResultsController : Controller
    {
        private readonly SalonDbContext _context;

        public AIResultsController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: AIResults
        public async Task<IActionResult> Index()
        {
            var salonDbContext = _context.AIResults.Include(a => a.Kullanici);
            return View(await salonDbContext.ToListAsync());
        }

        // GET: AIResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.AIResultId == id);
            if (aIResult == null)
            {
                return NotFound();
            }

            return View(aIResult);
        }

        // GET: AIResults/Create
        public IActionResult Create()
        {
            ViewBag.KullaniciId = new SelectList(_context.Kullanicilar, "KullaniciId", "Email");
            ViewBag.HairStyles = new List<SelectListItem>
            {
                new SelectListItem { Value = "101", Text = "Bangs (default)" },
                new SelectListItem { Value = "201", Text = "Long Hair" },
                new SelectListItem { Value = "301", Text = "Bangs with Long Hair" },
                new SelectListItem { Value = "401", Text = "Medium Hair Increase" },
                new SelectListItem { Value = "402", Text = "Light Hair Increase" },
                new SelectListItem { Value = "403", Text = "Heavy Hair Increase" },
                new SelectListItem { Value = "502", Text = "Light Curling" },
                new SelectListItem { Value = "503", Text = "Heavy Curling" },
                new SelectListItem { Value = "603", Text = "Short Hair (requires input size < 2048×2048)" },
                new SelectListItem { Value = "801", Text = "Blonde" },
                new SelectListItem { Value = "901", Text = "Straight Hair" },
                new SelectListItem { Value = "1001", Text = "Oil-free Hair (recommended for images with noticeable issues)" },
                new SelectListItem { Value = "1101", Text = "Hairline Fill (recommended for high hairline issues)" },
                new SelectListItem { Value = "1201", Text = "Smooth Hair (recommended for large hairstyle area)" },
                new SelectListItem { Value = "1301", Text = "Fill Hair Gap (fills the scalp part in the hair area)" }
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile uploadedFile, [Bind("KullaniciId,SuggestedStyle")] AIResult aiResult)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                ModelState.AddModelError("uploadedFile", "Lütfen bir resim yükleyin.");
                return View(aiResult);
            }

            using (var memoryStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memoryStream);
                aiResult.ImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
            }

            // API çağrısı ve gelen resmi kaydetme işlemleri
            string apiResultImage = await CallApiAndGetResult(aiResult.ImageBase64, aiResult.SuggestedStyle);

            if (!string.IsNullOrEmpty(apiResultImage))
            {
                ViewBag.ApiResponseImage = apiResultImage; // Görünümde göstermek için
                aiResult.ImageBase64 = apiResultImage;

                _context.Add(aiResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "API işleminde bir hata oluştu.");
            return View(aiResult);
        }


        private async Task<string> CallApiAndGetResult(string base64Image, string suggestedStyle)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.ailabtools.com/api/ai-portrait/effects/hairstyle-editor-pro/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "kXYapsHNnMRDmK79q7SxurGrkz1D4ypy0P5EvJxMjO6oeUg4XIwTiT9zbCHOY8Pi"); // API anahtarınızı buraya koyun

            var requestContent = new MultipartFormDataContent
    {
        { new StringContent(base64Image), "image" },
        { new StringContent(suggestedStyle), "styleId" }
    };

            var response = await client.PostAsync("apply", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                // Hata durumunda null döndürün veya hata mesajını kaydedin
                return null;
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponsee>(responseString);

            if (apiResponse != null && apiResponse.error_code == 0 && apiResponse.data != null)
            {
                return apiResponse.data.image; // Base64 formatında dönen resim
            }

            return null;
        }

        class ApiResponse
        {
            public string? Image { get; set; }
        }

        // GET: AIResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults.FindAsync(id);
            if (aIResult == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", aIResult.KullaniciId);
            return View(aIResult);
        }

        // POST: AIResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AIResultId,KullaniciId,SuggestedColor,SuggestedStyle,CreatedAt")] AIResult aIResult)
        {
            if (id != aIResult.AIResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aIResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AIResultExists(aIResult.AIResultId))
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "Email", aIResult.KullaniciId);
            return View(aIResult);
        }

        // GET: AIResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aIResult = await _context.AIResults
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.AIResultId == id);
            if (aIResult == null)
            {
                return NotFound();
            }

            return View(aIResult);
        }

        // POST: AIResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aIResult = await _context.AIResults.FindAsync(id);
            if (aIResult != null)
            {
                _context.AIResults.Remove(aIResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AIResultExists(int id)
        {
            return _context.AIResults.Any(e => e.AIResultId == id);
        }
    }
}

// POST: AIResults/Create
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
/*[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(IFormFile uploadedFile, [Bind("KullaniciId,SuggestedStyle")] AIResult aIResult)
{
    if (uploadedFile == null || uploadedFile.Length == 0)
    {
        ModelState.AddModelError("uploadedFile", "Lütfen bir resim yükleyin.");
        return View(aIResult);
    }

    using (var memoryStream = new MemoryStream())
    {
        await uploadedFile.CopyToAsync(memoryStream);
        aIResult.ImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
    }

    // API çağrısı ve gelen resmi kaydetme işlemleri
    string apiResultImage = await CallApiAndGetResult(aIResult.ImageBase64, aIResult.SuggestedStyle);

    if (!string.IsNullOrEmpty(apiResultImage))
    {
        ViewBag.ApiResponseImage = apiResultImage;
        aIResult.ImageBase64 = apiResultImage;

        _context.Add(aIResult);
        await _context.SaveChangesAsync();
        return View(aIResult);
    }

    ModelState.AddModelError("", "API işleminde bir hata oluştu.");
    return View(aIResult);
}
*/