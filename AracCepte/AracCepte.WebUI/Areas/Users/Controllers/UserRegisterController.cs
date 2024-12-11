using AracCepte.WebUI.Areas.Users.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserRegisterController : Controller
    {

        private readonly HttpClient _httpClient;

        public UserRegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.GetAsync("api/Users/register");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var registerModel = JsonSerializer.Deserialize<RegisterViewModel>(responseBody);
                        return View(registerModel);
                    }
                    else
                    {
                        ModelState.AddModelError("", "API'den gecerli bir cevap alinamadi.");
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata olustu: " + ex.Message);
                    return View("Error");
                }
            }
            return View(model);
        }
    }
}
