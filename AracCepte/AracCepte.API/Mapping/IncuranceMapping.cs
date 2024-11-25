using AutoMapper;
using AracCepte.DTO.DTOs.InsuranceDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class IncuranceMapping : Profile
    {
        public IncuranceMapping()
        {
            CreateMap<CreateInsuranceDto, Insurance>().ReverseMap();
            CreateMap<UpdateInsuranceDto, Insurance>().ReverseMap();
        }
    }
}
