using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.RatingDtos
{
    public class CreateRatingDto
    {
        public int SenderUserID { get; set; } //Sender's Rating ID
        public int ReceiverUserID { get; set; } // Receiver's Rating ID
        public int Point { get; set; } //Point value
        public string Comment { get; set; } // Comment
    }
}