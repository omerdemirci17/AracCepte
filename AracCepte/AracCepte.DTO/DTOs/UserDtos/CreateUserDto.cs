using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.UserDtos
{
    public class CreateUserDto
    {
        public string Name { get; set; } //User's Name
        public string Surname { get; set; } //User's Surname
        public string Email { get; set; }   //User's Email
        public int PhoneNumber { get; set; } // User's Phone Number
        public string UserType { get; set; } //Renter or VeihlesOwner
        public string ImageURL1 { get; set; } // User's Profile Photograph
    }
}
