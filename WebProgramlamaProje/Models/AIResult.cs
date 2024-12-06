namespace WebProgramlamaProje.Models
{
    public class AIResult
    {
        public int AIResultId { get; set; }
        public string ModelName { get; set; }
        public string SuggestedColor { get; set; }
        public string SuggestedStyle { get; set; }
        public DateTime CreatedAt { get; set; }

        // Kullanıcı ile ilişki
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
    }

}
