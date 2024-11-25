using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.WebUI.DTOs.ContactDtos
{
    public class UpdateContactDto
    {
        public int ContactId { get; set; }
        public string MapUrl { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
