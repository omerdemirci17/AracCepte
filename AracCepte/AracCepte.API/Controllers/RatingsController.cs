using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.RatingDtos;
using AracCepte.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController(IGenericService<Rating> _ratingService, IMapper _mapper) : ControllerBase
    {
        //Rating get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _ratingService.TGetList();
            return Ok(values);
        }

        //Rating get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _ratingService.TGetById(id);
            return Ok(value);
        }

        //Rating delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _ratingService.TDelete(id);
            return Ok("puanlama Silindi");
        }

        // Rating Create
        [HttpPost]
        public IActionResult Create(CreateRatingDto createRatingDto)
        {
            var newValue = _mapper.Map<Rating>(createRatingDto);
            _ratingService.TCreate(newValue);
            return Ok("Yeni puanlama Oluşturuldu");
        }

        //Rating Update
        [HttpPut]
        public IActionResult Update(UpdateRatingDto updateRatingDto)
        {
            var value = _mapper.Map<Rating>(updateRatingDto);
            _ratingService.TUpdate(value);
            return Ok("puanlama Güncellendi");
        }
    }
}
