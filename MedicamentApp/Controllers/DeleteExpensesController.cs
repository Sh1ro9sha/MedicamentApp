using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteExpensesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteExpensesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteExpenses
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expenses
                .Include(e => e.Drug)
                .Select(e => new DeleteExpensesViewModel
                {
                    Идентификатор = e.Идентификатор,
                    Идентификатор_лекарства = e.Идентификатор_лекарства,
                    Наименование_лекарства = e.Drug.Наименование,
                    Дата_реализации = e.Дата_реализации,
                    Количество = e.Количество,
                    Отпускная_цена = e.Отпускная_цена
                })
                .ToListAsync();

            return View(expenses);
        }

        // POST: /DeleteExpenses/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
