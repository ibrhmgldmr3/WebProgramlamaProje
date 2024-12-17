using System.ComponentModel.DataAnnotations;
using WebProgProje.Models;

namespace WebProgramlamaProje.Models
{
    public class Calisan
    {
        public int CalisanId { get; set; }
        public Kullanici? Kullanici { get; set; }
        public int? KullaniciId { get; set; }
        [Display(Name = "Çalışanın Adı")]
        public string Ad { get; set; }
        [Display(Name = "Çalışanın Soyadı")]
        public string Soyad { get; set; }
        [Display(Name = "Uzmanlık Alanı")]
        public int UzmanlikId { get; set; }  // Uzmanlık alanı referansı
        public Uzmanlik Uzmanlik { get; set; }  // İlişkili Uzmanlık
        public int? SalonId { get; set; }
        public Salon? Salon { get; set; }
        [Display(Name = "Çalışma Saati Giriş")]
        public TimeSpan CalismaSaatiGiris { get; set; }
        [Display(Name = "Çalışma Saati Çıkış")]
        public TimeSpan CalismaSaatiCikis { get; set; }
        public List<CalisanUygunluk>? CalisanUygunluklar { get; set; }
        public List<Randevu>? Randevular { get; set; }


    }


}
