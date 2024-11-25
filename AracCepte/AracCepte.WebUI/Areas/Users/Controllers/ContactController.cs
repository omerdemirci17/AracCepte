using Microsoft.AspNetCore.Mvc;
using AracCepte.WebUI.Areas.Users.Models;
using AracCepte.Entity.Entities;

namespace AracCepte.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactFormModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Doğrulama başarılı
                // Veritabanı işlemleri veya başka işlemler burada yapılabilir
                return RedirectToAction("Success");
            }
            else
            {
                // Model geçersiz
                return View("Contact"); // Hatalı model ile yeniden formu göster
            }

        }
    }
    
}
