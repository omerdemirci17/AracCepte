using AutoMapper;
using AracCepte.DTO.DTOs.VehicleDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class VehicleMapping : Profile
    {
        public VehicleMapping()
        {
            CreateMap<CreateVehicleDto, Vehicle>().ReverseMap();
            CreateMap<UpdateVehicleDto, Vehicle>().ReverseMap();
        }
    }
}
