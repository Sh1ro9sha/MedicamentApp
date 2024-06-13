using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddPharmacistsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddPharmacistsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddPharmacists/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddPharmacists/AddPharmacists
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPharmacists(AddPharmacistsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pharmacist = new Pharmacists
                {
                    Идентификатор = model.Идентификатор,
                    ФИО = model.ФИО,
                    Дата_приема = model.Дата_приема,
                    Статус = model.Статус
                };
                    _context.Pharmacists.Add(pharmacist);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "MenuMain");
                }

            return View("Index", model);
        }
    }
}
   