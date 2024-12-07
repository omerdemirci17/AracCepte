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


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7133/api/Users/register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Başarılı");
            }

            ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu");
            return View(model);
        }
    }

}
