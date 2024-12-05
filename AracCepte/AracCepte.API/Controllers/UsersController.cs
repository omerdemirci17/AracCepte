using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.UserDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IGenericService<User> _userService, IMapper _mapper) : ControllerBase
    {
        //User get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _userService.TGetList();
            return Ok(values);
        }

        //User  get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _userService.TGetById(id);
            return Ok(value);
        }

        //User delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _userService.TDelete(id);
            return Ok("kullanici Silindi");
        }

        // USer Create
        [HttpPost]
        public IActionResult Create(CreateUserDto createUserDto)
        {
            var newValue = _mapper.Map<User>(createUserDto);
            _userService.TCreate(newValue);
            return Ok("Yeni kullanici Oluşturuldu");
        }

        //User Update
        [HttpPut]
        public IActionResult Update(UpdateUserDto updateUserDto)
        {
            var value = _mapper.Map<User>(updateUserDto);
            _userService.TUpdate(value);
            return Ok("kullanici Güncellendi");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("");
            }

            
        }
    }
}
