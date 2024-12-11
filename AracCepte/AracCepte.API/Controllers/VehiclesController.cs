using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.VehicleDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(IGenericService<Vehicle> _vehicleService, IMapper _mapper) : ControllerBase
    {
        //Vecihle get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _vehicleService.TGetList();
            return Ok(values);
        }

        //Vecihle  get by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _vehicleService.TGetById(id);
            return Ok(value);
        }

        //Vecihle delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _vehicleService.TDelete(id);
            return Ok("arac Silindi");
        }

        // Vecihle Create
        [HttpPost]
        public IActionResult Create(CreateVehicleDto createVehicleDto)
        {
            var newValue = _mapper.Map<Vehicle>(createVehicleDto);
            _vehicleService.TCreate(newValue);
            return Ok("Yeni arac Oluşturuldu");
        }

        //Vecihle Update
        [HttpPut]
        public IActionResult Update(UpdateVehicleDto updateVehicleDto)
        {
            var value = _mapper.Map<Vehicle>(updateVehicleDto);
            _vehicleService.TUpdate(value);
            return Ok("arac Güncellendi");
        }
    }
}
