using AutoMapper;
using AracCepte.DTO.DTOs.SocialMediaDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class SocialMediaMapping : Profile
    {
        public SocialMediaMapping()
        {
            CreateMap<CreateSocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<UpdateSocialMediaDto, SocialMedia>().ReverseMap();
        }
    }
}
