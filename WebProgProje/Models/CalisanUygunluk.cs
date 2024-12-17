using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class CalisanUygunluk
    {
        public int CalisanUygunlukId { get; set; }
        [Display(Name = "Çalışan")]
        public int? CalisanId { get; set; }
        public Calisan? Calisan { get; set; }
        [Display(Name = "Gün")]
        public DayOfWeek Gun { get; set; }
        [Display(Name = "Başlangıç Saati")]
        public TimeSpan Baslangic { get; set; }
        [Display(Name = "Bitiş Saati")]
        public TimeSpan Bitis { get; set; }
    }


}