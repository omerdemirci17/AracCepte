using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.SocialMediaDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController(IGenericService<SocialMedia> _socialMediaService,IMapper _mapper) : ControllerBase
    {
        //Social Media get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _socialMediaService.TGetList();
            return Ok(values);
        }

        //Social Media  get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _socialMediaService.TGetById(id);
            return Ok(value);
        }

        //Social Media  delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _socialMediaService.TDelete(id);
            return Ok("Sosyal Medya Alanı Silindi");
        }

        // Social Media Create
        [HttpPost]
        public IActionResult Create(CreateSocialMediaDto createSocialMediaDto)
        {
            var newValue = _mapper.Map< SocialMedia > (createSocialMediaDto);
            _socialMediaService.TCreate(newValue);
            return Ok("Yeni Sosyal Medya Alanı Oluşturuldu");
        }

        //Social Media Update
        [HttpPut]
        public IActionResult Update(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var value = _mapper.Map< SocialMedia > (updateSocialMediaDto);
            _socialMediaService.TUpdate(value);
            return Ok("Sosyal Medya Alanı Güncellendi");
        }
    }
}

