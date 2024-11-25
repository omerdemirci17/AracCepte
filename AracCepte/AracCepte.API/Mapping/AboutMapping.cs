using AutoMapper;
using AracCepte.DTO.DTOs.AboutDtos;
using AracCepte.Entity.Entities;
using System.Runtime;
namespace AracCepte.API.Mapping
{
    public class AboutMapping : Profile 
    {

        public AboutMapping()
        {
            CreateMap<CreateAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();
        }
    }

}
