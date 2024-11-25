using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.InsuranceDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController(IGenericService<Insurance> _insuranceService, IMapper _mapper) : ControllerBase
    {
        //Insurance get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _insuranceService.TGetList();
            return Ok(values);
        }

        //Insurance get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _insuranceService.TGetById(id);
            return Ok(value);
        }

        //Insurance delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _insuranceService.TDelete(id);
            return Ok("sigorta Silindi");
        }

        // Insurance Create
        [HttpPost]
        public IActionResult Create(CreateInsuranceDto createInsuranceDto)
        {
            var newValue = _mapper.Map<Insurance>(createInsuranceDto);
            _insuranceService.TCreate(newValue);
            return Ok("Yeni sigorta  Oluşturuldu");
        }

        //Insurance Update
        [HttpPut]
        public IActionResult Update(UpdateInsuranceDto updateInsuranceDto)
        {
            var value = _mapper.Map<Insurance>(updateInsuranceDto);
            _insuranceService.TUpdate(value);
            return Ok("Sigorta Güncellendi");
        }
    }
}
