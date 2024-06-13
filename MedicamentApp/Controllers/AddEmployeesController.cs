using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddEmployeesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddEmployeesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddClients/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddClients/AddClients
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployees(AddEmployeesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employees = new Employees
                {
                    Идентификатор = model.Идентификатор,
                    ФИО = model.ФИО,
                    Должность = model.Должность,
                    Код_роли = model.Код_роли
                };

                _context.Employees.Add(employees);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "MenuMain");
            }

            return View("Index", model);
        }
    }
}
