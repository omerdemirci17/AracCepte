using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int ReservationID { get; set; } // Payment for Reservation ID
        public float PaymentAmount { get; set; } // Total Payment Amount
        public string Status { get; set; } // Payment's status
    }
}