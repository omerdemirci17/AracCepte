using AutoMapper;
using AracCepte.DTO.DTOs.ReservationDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class ReservationMapping : Profile
    {
        public ReservationMapping()
        {
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();
        }
    }
}
