using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.PaymentDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IGenericService<Payment> _paymentService, IMapper _mapper) : ControllerBase
    {
        //Payment get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _paymentService.TGetList();
            return Ok(values);
        }

        //Payment get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _paymentService.TGetById(id);
            return Ok(value);
        }

        //Payment delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _paymentService.TDelete(id);
            return Ok("odeme  Silindi");
        }

        // Payment Create
        [HttpPost]
        public IActionResult Create(CreatePaymentDto createPaymentDto)
        {
            var newValue = _mapper.Map<Payment>(createPaymentDto);
            _paymentService.TCreate(newValue);
            return Ok("Yeni odeme  Oluşturuldu");
        }

        //Payment Update
        [HttpPut]
        public IActionResult Update(UpdatePaymentDto updatePaymentDto)
        {
            var value = _mapper.Map<Payment>(updatePaymentDto);
            _paymentService.TUpdate(value);
            return Ok("odeme Güncellendi");
        }
    }
}

