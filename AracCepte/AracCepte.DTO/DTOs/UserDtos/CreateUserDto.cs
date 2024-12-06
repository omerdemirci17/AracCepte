using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.UserDtos
{
    public class CreateUserDto
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
        [RegularExpression(@"^\+?\d{10}$", ErrorMessage = "Girilen telefon numarası geçersizdir")]
        public int PhoneNumber { get; set; } // User's Phone Number
        public string ImageURL1 { get; set; } // User's Profile Photograph
    }
}
