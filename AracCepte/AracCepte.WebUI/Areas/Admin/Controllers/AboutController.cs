using Microsoft.AspNetCore.Mvc;
using AracCepte.WebUI.DTOs.AboutDtos;

namespace AracCepte.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}