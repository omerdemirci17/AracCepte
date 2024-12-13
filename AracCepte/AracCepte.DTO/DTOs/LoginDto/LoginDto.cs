using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.LoginDto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email adresi formatı yanlış ___@___.___ formatında olmalıdır")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
