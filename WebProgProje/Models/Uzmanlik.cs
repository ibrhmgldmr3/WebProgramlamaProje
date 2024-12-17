using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Models
{
    public class Uzmanlik
    {
        public int UzmanlikId { get; set; }
        public string Ad { get; set; }

        // Bir uzmanlık birden fazla çalışana sahip olabilir
        public List<Calisan> Calisanlar { get; set; } = new List<Calisan>();
    }

}