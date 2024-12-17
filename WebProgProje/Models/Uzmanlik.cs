using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class Uzmanlik
    {
        public int UzmanlikId { get; set; }

        [Display(Name = "Uzmanlık Adı")]
        public string Ad { get; set; }

        public List<Islem>? Islemler { get; set; } // Uzmanlığın işlemleri
        public List<Calisan>? Calisanlar { get; set; } // Uzmanlığa bağlı çalışanlar
    }
}