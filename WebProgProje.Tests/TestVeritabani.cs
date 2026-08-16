using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

namespace WebProgProje.Tests
{
    /// <summary>
    /// Testler için InMemory sağlayıcısıyla izole bir <see cref="SalonDbContext"/> üretir.
    /// Her test kendi veritabanı adını kullandığı için testler birbirini etkilemez.
    /// </summary>
    public static class TestVeritabani
    {
        public static SalonDbContext YeniContext()
        {
            var options = new DbContextOptionsBuilder<SalonDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SalonDbContext(options);
        }

        /// <summary>
        /// Tek salon, tek çalışan, iki işlem ve iki randevudan oluşan küçük bir veri seti kurar.
        /// Beklenen toplam kazanç: 150 + 300 = 450.
        /// </summary>
        public static SalonDbContext OrnekVeriliContext(DateOnly randevuTarihi)
        {
            var context = YeniContext();

            var salon = new Salon
            {
                SalonId = 1,
                Isim = "Test Salon",
                Adres = "Sakarya",
                Telefon = "02640000000",
                Tip = "Kuaför",
                CalismaBaslangic = new TimeSpan(9, 0, 0),
                CalismaBitis = new TimeSpan(19, 0, 0)
            };

            var uzmanlik = new Uzmanlik { UzmanlikId = 1, Ad = "Saç Kesimi" };

            var kullanici = new Kullanici
            {
                KullaniciId = 1,
                Email = "musteri@test.com",
                PasswordHash = "gizli-parola",
                Role = "Member",
                FullName = "Test Musteri",
                PhoneNumber = "05000000000"
            };

            var calisan = new Calisan
            {
                CalisanId = 1,
                Ad = "Ayse",
                Soyad = "Yilmaz",
                SalonId = 1,
                UzmanlikId = 1
            };

            var sacKesimi = new Islem
            {
                IslemId = 1,
                Ad = "Saç Kesimi",
                Sure = new TimeSpan(0, 30, 0),
                Ucret = 150m
            };

            var boya = new Islem
            {
                IslemId = 2,
                Ad = "Boya",
                Sure = new TimeSpan(1, 30, 0),
                Ucret = 300m
            };

            context.Salonlar.Add(salon);
            context.Uzmanliklar.Add(uzmanlik);
            context.Kullanicilar.Add(kullanici);
            context.Calisanlar.Add(calisan);
            context.Islemler.AddRange(sacKesimi, boya);

            context.Randevular.AddRange(
                new Randevu
                {
                    RandevuId = 1,
                    CalisanId = 1,
                    IslemId = 1,
                    SalonId = 1,
                    KullaniciId = 1,
                    Tarih = randevuTarihi,
                    Saat = new TimeSpan(10, 0, 0),
                    OnaylandiMi = true
                },
                new Randevu
                {
                    RandevuId = 2,
                    CalisanId = 1,
                    IslemId = 2,
                    SalonId = 1,
                    KullaniciId = 1,
                    Tarih = randevuTarihi.AddDays(1),
                    Saat = new TimeSpan(14, 0, 0),
                    OnaylandiMi = false
                });

            context.CalisanUygunluklar.Add(new CalisanUygunluk
            {
                CalisanUygunlukId = 1,
                CalisanId = 1,
                Gun = DayOfWeek.Monday,
                Baslangic = new TimeSpan(9, 0, 0),
                Bitis = new TimeSpan(18, 0, 0)
            });

            context.SaveChanges();

            return context;
        }
    }
}
