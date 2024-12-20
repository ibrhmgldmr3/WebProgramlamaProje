using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class Islem
    {
        public int IslemId { get; set; }
        [Display(Name = "İşlem Adı")]
        public string Ad { get; set; }
        public TimeSpan Sure { get; set; }
        public decimal Ucret { get; set; }

        // İşlemin yapılabileceği uzmanlıklar
        public List<IslemUzmanlik> IslemUzmanliklar { get; set; } = new List<IslemUzmanlik>();
    }
}
