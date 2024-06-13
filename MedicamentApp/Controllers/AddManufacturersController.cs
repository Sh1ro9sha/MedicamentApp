using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddManufacturersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddManufacturersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddManufacturers/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddManufacturers/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddManufacturersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = new Manufacturers
                {
                    Идентификатор = model.Идентификатор,
                    Название = model.Название,
                    Адрес = model.Адрес,
                    Контактный_телефон = model.Контактный_телефон
                };
                    _context.Manufacturers.Add(manufacturer);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "MenuMain");
            }
            return View(model);
        }
    }
}
