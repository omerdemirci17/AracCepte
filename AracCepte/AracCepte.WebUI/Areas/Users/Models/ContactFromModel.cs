using System.ComponentModel.DataAnnotations;
using AracCepte.DataAccess.Context;


namespace AracCepte.WebUI.Areas.Users.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Adınızı giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-postanızı giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numaranizi giriniz")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mesajınızı giriniz.")]
        [StringLength(500,ErrorMessage = "Mesaj en fazla 500 karakter uzunluğunda olmalıdır.")]
        public string Comment { get; set; }
    }

}
