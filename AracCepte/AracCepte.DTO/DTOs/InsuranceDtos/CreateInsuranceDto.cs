using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DTO.DTOs.InsuranceDtos
{
    public class CreateInsuranceDto
    {
        public int VehiclesId { get; set; } //Vehicle ID to be Insured
        public DateTime InsuranceStartDate { get; set; } // Insurance Start Date
        public DateTime InsuranceEndDate { get; set; } // Insurance End Date
    }
}