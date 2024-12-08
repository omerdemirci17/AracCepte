using AracCepte.WebUI.Areas.Users.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;


namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserRegisterController : Controller
    {

        private readonly HttpClient _httpClient;

        public UserRegisterController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        public async Task<IActionResult> Register1()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7133/swagger/api/Users/register");
            return View(response);
        }
    }

}
