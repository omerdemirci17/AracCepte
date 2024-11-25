using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserRegisterController : Controller
    {
        public IActionResult Register()
        {
            return View("Register");
        }
    }

}
