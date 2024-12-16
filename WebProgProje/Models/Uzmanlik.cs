using WebProgramlamaProje.Models;

namespace WebProgProje.Models
{
    public class Uzmanlik
    {
        public int UzmanlikId { get; set; }
        public string Ad { get; set; }

        public List<Islem> Islemler = new List<Islem>();

    }
}
