using AutoMapper;
using AracCepte.DTO.DTOs.RatingDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class RatingMapping : Profile
    {
        public RatingMapping() 
        {
            CreateMap<CreateRatingDto, Rating>().ReverseMap();
            CreateMap<UpdateRatingDto, Rating>().ReverseMap();

        }
    }
}
