using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    public class UserProfileController : Controller
    {
        [Area("Users")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult UserProfile()
        {
            return View();
        }
    }
}
