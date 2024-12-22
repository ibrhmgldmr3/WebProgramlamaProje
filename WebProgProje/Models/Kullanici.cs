using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebProgramlamaProje.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Lütfen email adresi giriniz!")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email giriniz!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz!")]
        public string PasswordHash { get; set; }
        public string? Role { get; set; } // "Admin" "Musteri" "Calisan"

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen adınızı soyadınızı giriniz!")]
        [MaxLength(25)]
        public string FullName { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Lütfen telefon numarınızı giriniz!")]
        [Phone(ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz!")]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Telefon numarası yalnızca rakamlardan oluşmalıdır ve 11 haneli olmalıdır!")]
        public string PhoneNumber { get; set; }

        // Kullanıcının randevuları
        public List<Randevu>? Randevular { get; set; } = new List<Randevu>();

        public string? ProfilResmi { get; set; }


        // public List<Islem> YaptirdiğiIslemlemler { get; set; } = new List<Islem>();

        // Kullanıcının yapay zeka sonuçları
        public List<AIResult> AIResults { get; set; } = new List<AIResult>();
    }

}