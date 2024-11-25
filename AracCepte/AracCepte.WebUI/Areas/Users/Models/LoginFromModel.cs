using System.ComponentModel.DataAnnotations;
using AracCepte.DataAccess.Context;


namespace AracCepte.WebUI.Areas.Users.Models
{
    public class LoginFromModel
    {
        [Required(ErrorMessage = " Lutfen Emailinizi giriniz")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifrenizi giriniz")]
        public string Password { get; set; }
    }
}
