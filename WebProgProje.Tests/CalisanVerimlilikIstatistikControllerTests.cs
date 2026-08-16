using Microsoft.AspNetCore.Mvc;
using WebProgramlamaProje.Controllers;
using Xunit;

namespace WebProgProje.Tests
{
    /// <summary>
    /// İstatistik REST API'sinin LINQ toplulaştırma sorgularını doğrular.
    /// </summary>
    public class CalisanVerimlilikIstatistikControllerTests
    {
        private static readonly DateOnly TestTarihi = new DateOnly(2025, 3, 10);

        [Fact]
        public async Task GetCalisanVerimlilik_CalisanBasinaRandevuSayisiVeToplamKazanciDondurur()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetCalisanVerimlilik();

            var ok = Assert.IsType<OkObjectResult>(sonuc);
            var veri = JsonYardimcisi.Ayristir(ok.Value);

            Assert.Equal(1, veri.GetArrayLength());

            var calisan = veri[0];
            Assert.Equal("Ayse", calisan.GetProperty("Ad").GetString());
            Assert.Equal(2, calisan.GetProperty("RandevuSayisi").GetInt32());
            Assert.Equal(450m, calisan.GetProperty("ToplamKazanc").GetDecimal());
        }

        [Fact]
        public async Task GetGunlukKazanc_YalnizcaVerilenTarihtekiRandevulariToplar()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetGunlukKazanc(TestTarihi.ToDateTime(TimeOnly.MinValue));

            var ok = Assert.IsType<OkObjectResult>(sonuc);
            var veri = JsonYardimcisi.Ayristir(ok.Value);

            // O gün yalnızca 150 TL'lik saç kesimi randevusu var; 300 TL'lik boya ertesi gün.
            Assert.Equal(150m, veri[0].GetProperty("GunlukKazanc").GetDecimal());
        }

        [Fact]
        public async Task GetRandevuIstatistikleri_TarihAraligiDisindakiRandevulariHaricTutar()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            // Aralık yalnızca ilk günü kapsıyor.
            var sonuc = await controller.GetRandevuIstatistikleri(TestTarihi, TestTarihi);

            var ok = Assert.IsType<OkObjectResult>(sonuc);
            var veri = JsonYardimcisi.Ayristir(ok.Value);

            Assert.Equal(1, veri.GetArrayLength());
            Assert.Equal(1, veri[0].GetProperty("ToplamRandevuSayisi").GetInt32());
            Assert.Equal(150m, veri[0].GetProperty("ToplamKazanc").GetDecimal());
        }

        [Fact]
        public async Task GetSalonIslemIstatistikleri_SalonVeIslemKiriliminaGoreGruplar()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetSalonIslemIstatistikleri();

            var ok = Assert.IsType<OkObjectResult>(sonuc);
            var veri = JsonYardimcisi.Ayristir(ok.Value);

            // İki farklı işlem → iki grup.
            Assert.Equal(2, veri.GetArrayLength());
            foreach (var satir in veri.EnumerateArray())
            {
                Assert.Equal("Test Salon", satir.GetProperty("SalonAdi").GetString());
                Assert.Equal(1, satir.GetProperty("ToplamIslemSayisi").GetInt32());
            }
        }

        [Fact]
        public async Task GetCalisanUygunluk_KayitYoksaNotFoundDondurur()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetCalisanUygunluk(calisanId: 999);

            Assert.IsType<NotFoundObjectResult>(sonuc);
        }

        [Fact]
        public async Task GetKullaniciRandevular_KullanicininRandevulariniDondurur()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetKullaniciRandevular(kullaniciId: 1);

            var ok = Assert.IsType<OkObjectResult>(sonuc);
            var veri = JsonYardimcisi.Ayristir(ok.Value);

            Assert.Equal(2, veri.GetArrayLength());
            Assert.Equal("Test Salon", veri[0].GetProperty("SalonAdi").GetString());
        }

        [Fact]
        public async Task GetKullaniciRandevular_RandevusuOlmayanKullaniciIcinNotFoundDondurur()
        {
            using var context = TestVeritabani.OrnekVeriliContext(TestTarihi);
            var controller = new CalisanVerimlilikIstatistikController(context);

            var sonuc = await controller.GetKullaniciRandevular(kullaniciId: 42);

            Assert.IsType<NotFoundObjectResult>(sonuc);
        }
    }
}
