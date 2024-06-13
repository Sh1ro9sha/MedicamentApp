using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddExpensesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddExpensesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddExpenses/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddExpenses/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddExpensesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var expense = new Expenses
                {
                    Идентификатор = model.Идентификатор,
                    Идентификатор_лекарства = model.Идентификатор_лекарства,
                    Дата_реализации = model.Дата_реализации,
                    Количество = model.Количество,
                    Отпускная_цена = model.Отпускная_цена
                };

                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "MenuMain");
            }

            return View(model);
        }
    }
}
