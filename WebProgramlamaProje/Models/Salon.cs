using Microsoft.EntityFrameworkCore;

namespace WebProgramlamaProje.Models
{
    public class Salon 
    {
        public int SalonId { get; set; }
        public string Isim { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Tip { get; set; } // "Kuaför" veya "Berber"
        public TimeSpan CalismaBaslangic { get; set; }
        public TimeSpan CalismaBitis { get; set; }
        public List<Islem> Islemler { get; set; } = new List<Islem>();
        public List<Calisan> Calisanlar { get; set; } = new List<Calisan>();



    }
}
