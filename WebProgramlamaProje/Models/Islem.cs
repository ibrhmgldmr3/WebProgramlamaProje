namespace WebProgramlamaProje.Models
{
    public class Islem
    {
        public int IslemId { get; set; }
        public string Ad { get; set; }
        public TimeSpan Sure { get; set; }
        public decimal Ucret { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
    }

}
