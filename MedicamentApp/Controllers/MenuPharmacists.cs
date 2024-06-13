using Microsoft.AspNetCore.Mvc;

namespace MedicamentApp.Controllers
{
    public class MenuPharmacists : Controller
    {
        // GET: /Menu
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Menu
        [HttpPost]
        public IActionResult Index(string page)
        {
            if (!string.IsNullOrEmpty(page))
            {
                return Redirect(page);
            }
            else
            {
                // Если не выбрана страница, просто возвращаем текущую страницу
                return RedirectToAction("Index");
            }
        }
    }
}