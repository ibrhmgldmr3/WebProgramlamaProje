
using WebProgProje.Models;

namespace WebProgramlamaProje.Models
{
    public class IslemUzmanlik
    {
        public int IslemId { get; set; }
        public Islem Islem { get; set; }

        public int UzmanlikId { get; set; }
        public Uzmanlik Uzmanlik { get; set; }
    }
}

