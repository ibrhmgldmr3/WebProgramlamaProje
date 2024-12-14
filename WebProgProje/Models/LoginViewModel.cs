using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen email adresi giriniz!")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email giriniz!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz!")]
        public string Password { get; set; }
    }
}