using System.Security.Cryptography.X509Certificates;

namespace WebProgramlamaProje.Models
{
    public class AIResult
    {
        public int AIResultId { get; set; }
        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
        public string ?SuggestedColor { get; set; }
        public string ?SuggestedStyle { get; set; }
        public string ?ImageBase64 { get; set; }
        public DateTime CreatedAt { get; set; }



    }

}