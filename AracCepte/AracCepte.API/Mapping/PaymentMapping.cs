using AutoMapper;
using AracCepte.DTO.DTOs.PaymentDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Mapping
{
    public class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            CreateMap<CreatePaymentDto, Payment>().ReverseMap();
            CreateMap<UpdatePaymentDto, Payment>().ReverseMap();
        }
    }
}
