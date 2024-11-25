using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.Entity.Entities
{
    public class Payment
    {
        public int Id { get; set; } // Payment ID
        public int ReservationID { get; set; } // Payment for Reservation ID
        public float PaymentAmount { get; set; } // Total Payment Amount
        public string Status { get; set; } // Payment's status
    }
}
