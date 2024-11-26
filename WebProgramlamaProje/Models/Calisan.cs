namespace WebProgramlamaProje.Models
{
    public class Calisan
    {
        public int CalisanId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string UzmanlikAlani { get; set; } // Saç Kesimi, Saç Boyama vb.
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public List<CalisanUygunluk> UygunlukSaatleri { get; set; } = new List<CalisanUygunluk>();
    }

}
