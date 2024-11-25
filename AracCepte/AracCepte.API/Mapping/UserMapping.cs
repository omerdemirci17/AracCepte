using AutoMapper;
using AracCepte.DTO.DTOs.UserDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
        }
    }
}
