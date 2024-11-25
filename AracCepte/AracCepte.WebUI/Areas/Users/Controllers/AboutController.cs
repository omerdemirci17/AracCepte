using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    public class AboutController : Controller
    {
        [Area("Users")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult About()
        {
            return View("About");
        }
    }
}
