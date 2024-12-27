using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalisanVerimlilikIstatistikController : ControllerBase
    {
        private readonly SalonDbContext _context;

        public CalisanVerimlilikIstatistikController(SalonDbContext context)
        {
            _context = context;
        }

        [HttpGet("verimlilik")]
        public async Task<IActionResult> GetCalisanVerimlilik()
        {
            var verimlilik = await _context.Calisanlar
                .Select(c => new
                {
                    c.CalisanId,
                    c.Ad,
                    c.Soyad,
                    RandevuSayisi = c.Randevular.Count(),
                    ToplamKazanc = c.Randevular.Sum(r => r.Islem.Ucret)
                })
                .ToListAsync();

            return Ok(verimlilik);
        }

        [HttpGet("gunluk-kazanc")]
        public async Task<IActionResult> GetGunlukKazanc(DateTime tarih)
        {
            var gunlukKazanc = await _context.Calisanlar
                .Select(c => new
                {
                    c.CalisanId,
                    c.Ad,
                    c.Soyad,
                    GunlukKazanc = c.Randevular
                        .Where(r => r.Tarih == DateOnly.FromDateTime(tarih))
                        .Sum(r => r.Islem.Ucret)
                })
                .ToListAsync();

            return Ok(gunlukKazanc);
        }

        [HttpGet("salon/islem-istatistikleri")]
        public async Task<IActionResult> GetSalonIslemIstatistikleri()
        {
            var istatistikler = await _context.Randevular
                .GroupBy(r => new { r.Salon.Isim, r.Islem.Ad })
                .Select(g => new
                {
                    SalonAdi = g.Key.Isim,
                    IslemAdi = g.Key.Ad,
                    ToplamIslemSayisi = g.Count(),
                    ToplamKazanc = g.Sum(r => r.Islem.Ucret)
                })
                .ToListAsync();

            return Ok(istatistikler);
        }

        [HttpGet("randevu-istatistikleri")]
        public async Task<IActionResult> GetRandevuIstatistikleri(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var istatistikler = await _context.Randevular
                .Where(r => r.Tarih >= DateOnly.FromDateTime(baslangicTarihi) && r.Tarih <= DateOnly.FromDateTime(bitisTarihi))
                .GroupBy(r => r.Tarih)
                .Select(g => new
                {
                    Tarih = g.Key,
                    ToplamRandevuSayisi = g.Count(),
                    ToplamKazanc = g.Sum(r => r.Islem.Ucret)
                })
                .ToListAsync();

            return Ok(istatistikler);
        }

        [HttpGet("calisan/calisma-saatleri")]
        public async Task<IActionResult> GetCalisanCalismaSaatleri()
        {
            var calismaSaatleri = await _context.CalisanUygunluklar
                .Include(cu => cu.Calisan)
                .Select(cu => new
                {
                    cu.Calisan.Ad,
                    cu.Calisan.Soyad,
                    cu.Gun,
                    cu.Baslangic,
                    cu.Bitis
                })
                .ToListAsync();

            return Ok(calismaSaatleri);
        }
    }
}

