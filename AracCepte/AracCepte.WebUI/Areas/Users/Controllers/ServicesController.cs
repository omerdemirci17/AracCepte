﻿using Microsoft.AspNetCore.Mvc;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ServicesController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }
    }
}
