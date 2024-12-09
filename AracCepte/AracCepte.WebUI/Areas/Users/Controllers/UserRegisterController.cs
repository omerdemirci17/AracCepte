using AracCepte.WebUI.Areas.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;


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
            var response = await _httpClient.GetStringAsync("api/Users/register");
            var registerModel = JsonSerializer.Deserialize<RegisterViewModel>(response);
            return View(registerModel);
        }
    }

}
