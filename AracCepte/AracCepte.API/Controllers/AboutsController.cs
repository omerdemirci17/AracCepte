using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.AboutDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController(IGenericService<About> _aboutService, IMapper  _mapper) : ControllerBase
    {
        //About get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _aboutService.TGetList();
            return Ok(values);
        }

        //About get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _aboutService.TGetById(id);
            return Ok(value);
        }

        //About delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _aboutService.TDelete(id);
            return Ok("Hakkımızda Alanı Silindi");
        }

        // About Create
        [HttpPost]
        public IActionResult Create(CreateAboutDto createAboutDto)
        {
            var newValue = _mapper.Map<About>(createAboutDto);
            _aboutService.TCreate(newValue);
            return Ok("Yeni Hakkımızda Alanı Oluşturuldu");
        }

        //About Update
        [HttpPut]
        public IActionResult Update(UpdateAboutDto updateAboutDto)
        {
            var value = _mapper.Map<About>(updateAboutDto);
            _aboutService.TUpdate(value);
            return Ok("Hakkımda Alanı Güncellendi");
        }

    }
}
