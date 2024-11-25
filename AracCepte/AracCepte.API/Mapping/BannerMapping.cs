using AutoMapper;
using AracCepte.DTO.DTOs.BannerDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class BannerMapping : Profile
    {
        public BannerMapping()
        {
            CreateMap<CreateBannerDto, Banner>().ReverseMap();
            CreateMap<UpdateBannerDto, Banner>().ReverseMap();
        }
    }
}
