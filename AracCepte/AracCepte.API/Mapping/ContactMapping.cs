using AutoMapper;
using AracCepte.DTO.DTOs.ContactDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<CreateContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();

        }
    }
}
