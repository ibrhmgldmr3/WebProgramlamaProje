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
                .Include(r => r.Salon)
                .Include(r => r.Islem)
                .Where(r => r.Salon != null && r.Islem != null)
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
        public async Task<IActionResult> GetRandevuIstatistikleri(DateOnly baslangicTarihi, DateOnly bitisTarihi)
        {
            var istatistikler = await _context.Randevular
                .Include(r => r.Islem)
                .Where(r => r.Tarih >= baslangicTarihi && r.Tarih <= bitisTarihi)
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
        [HttpGet("kullanici/{kullaniciId}/randevular")]
        public async Task<IActionResult> GetKullaniciRandevular(int kullaniciId)
        {
            var randevular = await _context.Randevular
                .Include(r => r.Islem)
                .Include(r => r.Salon)
                .Include(r => r.Calisan)
                .Where(r => r.KullaniciId == kullaniciId)
                .Select(r => new
                {
                    r.RandevuId,
                    r.Tarih,
                    r.Saat,
                    IslemAdi = r.Islem.Ad,
                    IslemUcreti = r.Islem.Ucret,
                    SalonAdi = r.Salon.Isim,
                    CalisanAdi = r.Calisan.Ad,
                    CalisanSoyadi = r.Calisan.Soyad,
                    r.OnaylandiMi
                })
                .ToListAsync();

            if (randevular == null || !randevular.Any())
            {
                return NotFound(new { Message = "Bu kullanıcıya ait randevu bulunamadı." });
            }

            return Ok(randevular);
        }

        [HttpGet("api/randevus/calisan/{calisanId}/uygunluk")]
        public async Task<IActionResult> GetCalisanUygunluk(int calisanId)
        {
            var uygunluklar = await _context.CalisanUygunluklar
                .Where(cu => cu.CalisanId == calisanId)
                .Select(cu => new
                {
                    cu.Gun,
                    cu.Baslangic,
                    cu.Bitis
                }).ToListAsync();

            if (uygunluklar == null || !uygunluklar.Any())
            {
                return NotFound(new { Message = "Bu çalışana ait uygunluk bilgisi bulunamadı." });
            }

            return Ok(uygunluklar);
        }


    }
}

