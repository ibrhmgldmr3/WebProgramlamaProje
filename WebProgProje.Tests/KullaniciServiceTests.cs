using WebProgramlamaProje.Models;
using Xunit;

namespace WebProgProje.Tests
{
    /// <summary>
    /// Kullanıcı servisinin e-posta ile arama davranışını doğrular.
    /// </summary>
    public class KullaniciServiceTests
    {
        private static Kullanici OrnekKullanici() => new Kullanici
        {
            KullaniciId = 1,
            Email = "musteri@test.com",
            PasswordHash = "gizli-parola",
            Role = "Member",
            FullName = "Test Musteri",
            PhoneNumber = "05000000000"
        };

        [Fact]
        public void GetKullaniciByEmail_KayitliKullaniciyiDondurur()
        {
            using var context = TestVeritabani.YeniContext();
            context.Kullanicilar.Add(OrnekKullanici());
            context.SaveChanges();

            var servis = new KullaniciService(context);

            var kullanici = servis.GetKullaniciByEmail("musteri@test.com");

            Assert.NotNull(kullanici);
            Assert.Equal("Test Musteri", kullanici!.FullName);
        }

        [Fact]
        public void GetKullaniciByEmail_KayitliOlmayanEmailIcinNullDondurur()
        {
            using var context = TestVeritabani.YeniContext();
            context.Kullanicilar.Add(OrnekKullanici());
            context.SaveChanges();

            var servis = new KullaniciService(context);

            var kullanici = servis.GetKullaniciByEmail("olmayan@test.com");

            Assert.Null(kullanici);
        }
    }
}
