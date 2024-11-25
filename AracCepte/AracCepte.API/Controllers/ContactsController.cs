using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracCepte.Business.Abstract;
using AracCepte.DTO.DTOs.ContactDtos;
using AracCepte.Entity.Entities;

namespace AracCepte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IGenericService<Contact> _contactService, IMapper _mapper) : ControllerBase
    {
        //Contact get all of them
        [HttpGet]
        public IActionResult Get()
        {
            var values = _contactService.TGetList();
            return Ok(values);
        }

        //Contact get by id
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var value = _contactService.TGetById(id);
            return Ok(value);
        }

        //Contact delete by id
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _contactService.TDelete(id);
            return Ok("Iletisim Alanı Silindi");
        }

        // Contact Create
        [HttpPost]
        public IActionResult Create(CreateContactDto createContactDto)
        {
            var newValue = _mapper.Map<Contact>(createContactDto);
            _contactService.TCreate(newValue);
            return Ok("Yeni Iletisim Alanı Oluşturuldu");
        }

        //Contact Update
        [HttpPut]
        public IActionResult Update(UpdateContactDto updateContactDto)
        {
            var value = _mapper.Map<Contact>(updateContactDto);
            _contactService.TUpdate(value);
            return Ok("Iletisim Alanı Güncellendi");
        }
    }
}
