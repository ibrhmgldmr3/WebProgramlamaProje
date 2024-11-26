namespace WebProgramlamaProje.Models
{
    public class AIResult
    {
        public int AIResultId { get; set; }
        public int UserId { get; set; }
        public User Kullanici { get; set; }
        public string ImageUrl { get; set; }
        public string OnerilenSacModeli { get; set; }
        public string OnerilenSacRengi { get; set; }
    }

}
