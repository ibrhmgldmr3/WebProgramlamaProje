using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebProgramlamaProje.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Admin" veya "Member"
        public string FullName { get; set; }
        [MaxLength(11)]
        
        public string PhoneNumber { get; set; }
        public List<Randevu> Randevular { get; set; } = new List<Randevu>();
    }

}
