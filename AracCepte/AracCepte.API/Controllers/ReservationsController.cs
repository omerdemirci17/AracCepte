using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.ReservationDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IGenericService<Reservation> _reservationService, IMapper _mapper) : ControllerBase
    {
        //Reservation get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _reservationService.TGetList();
            return Ok(values);
        }

        //Reservation  get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _reservationService.TGetById(id);
            return Ok(value);
        }

        //Reservation  delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _reservationService.TDelete(id);
            return Ok("rezervasyon Silindi");
        }

        // Reservation Create
        [HttpPost]
        public IActionResult Create(CreateReservationDto createReservationDto)
        {
            var newValue = _mapper.Map<Reservation>(createReservationDto);
            _reservationService.TCreate(newValue);
            return Ok("Yeni rezervasyon Oluşturuldu");
        }

        //Reservation Update
        [HttpPut]
        public IActionResult Update(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            _reservationService.TUpdate(value);
            return Ok("rezervasyon Güncellendi");
        }
    }
}
