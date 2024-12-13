using AracCepte.Business.AuthService;
using AracCepte.DTO.DTOs.LoginDto;
using Microsoft.AspNetCore.Mvc;

namespace AracCepte.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            _authService = new AuthService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var token = _authService.Authenticate(dto.Email, dto.Password);

            if (token == null)
            {
                return Unauthorized(new { message = " Email veya sifre yanlis"});
            }
            return Ok(new { token });
        }

    }
}
