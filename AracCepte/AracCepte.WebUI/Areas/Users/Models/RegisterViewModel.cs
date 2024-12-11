using System.ComponentModel.DataAnnotations;

namespace AracCepte.WebUI.Areas.Users.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; } //User's Name

        [Required]
        public string Surname { get; set; } //User's Surname

        [Required]
        [EmailAddress(ErrorMessage = "Email adresi formatı yanlış ___@___.___ formatında olmalıdır")]
        public string Email { get; set; }   //User's Email

        [Required]
        [MinLength(6, ErrorMessage = "Şifre minimum 6 karakterden oluşmalıdır ")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = " Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }

            [Required]
        [RegularExpression(@"^\+?\d{10}$", ErrorMessage = "Girilen telefon numarası geçersizdir")]
        public int PhoneNumber { get; set; } // User's Phone Number
    }
}
