using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AddVehicleController : Controller
    {
        public IActionResult AddVehicle()
        {
            return View("AddVehicle");
        }
    }
}
