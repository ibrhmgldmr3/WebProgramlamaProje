using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebProgramlamaProje.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ?Role { get; set; } // "Admin" "Musteri" "Calisan"
        public string FullName { get; set; }

        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        // Kullanıcının randevuları
        public List<Randevu> ?Randevular { get; set; } = new List<Randevu>();

        public byte[]? ProfilResmi { get; set; }


        // public List<Islem> YaptirdiğiIslemlemler { get; set; } = new List<Islem>();

        // Kullanıcının yapay zeka sonuçları
        //public List<AIResult> AIResult { get; set; } = new List<AIResult>();
    }

}
