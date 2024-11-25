using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.Entity.Entities
{
    public class Reservation
    {
        public int Id { get; set; } // Reservation Id
        public int VehiclesID { get; set; } //Reservated Veihle Id
        public int RenterID { get; set; } // Reservate Renter ID
        public DateTime ReservationStartDate { get; set; } //Reservation Start date
        public DateTime ReservationEndDate { get; set; } //Reservation End date
        public string Status { get; set; } // Reservaton Status
    }
}
