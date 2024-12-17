using System.ComponentModel.DataAnnotations;
using WebProgProje.Models;

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
        public List<Uzmanlik> Uzmanliklar { get; set; } = new List<Uzmanlik>();
    }


}
