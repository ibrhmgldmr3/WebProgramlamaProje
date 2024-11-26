namespace WebProgramlamaProje.Models
{
    public class CalisanUygunluk
    {
        public int UygunlukId { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public TimeSpan Baslangic { get; set; }
        public TimeSpan Bitis { get; set; }
    }

}
