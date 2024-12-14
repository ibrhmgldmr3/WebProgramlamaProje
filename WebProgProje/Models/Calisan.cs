namespace WebProgramlamaProje.Models
{
    public class Calisan
    {
        public int CalisanId { get; set; }
        public Kullanici ?Kullanici { get; set; }
        public int ?KullaniciId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Uzmanlik { get; set; } // Saç Kesimi, Saç Boyama vb.
        public int ?SalonId { get; set; }
        public Salon ?Salon { get; set; }
        public TimeSpan CalismaSaatiGiris { get; set; }
        public TimeSpan CalismaSaatiCikis { get; set; }
        public List<Randevu> ?Randevular { get; set; }

    }


}
