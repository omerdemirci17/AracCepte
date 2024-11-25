using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserHomeController : Controller
    {
        public IActionResult HomePage()
        {
            return View("HomePage");
        }
    }
    
    

}

