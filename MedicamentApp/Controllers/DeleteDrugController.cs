using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteDrugController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteDrugController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteDrug
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Drug
                .Select(p => new DeleteDrugViewModel
                {
                    Идентификатор = p.Идентификатор,
                    Наименование = p.Наименование,
                    Аннотация = p.Аннотация,
                    Идентификатор_производителя = p.Идентификатор_производителя,
                    Единица_измерения = p.Единица_измерения,
                    Место_хранения = p.Место_хранения
                })
                .ToListAsync();

            return View(products);
        }

        // POST: /DeleteDrug/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var drug = await _context.Drug.FindAsync(id);

            if (drug == null)
            {
                return NotFound();
            }

            // Удаляем все записи в Profit, связанные с данным лекарством
            var relatedProfits = _context.Profit.Where(p => p.Идентификатор_лекарства == id);
            _context.Profit.RemoveRange(relatedProfits);

            // Удаляем все записи в Expenses, связанные с данным лекарством
            var relatedExpenses = _context.Expenses.Where(e => e.Идентификатор_лекарства == id);
            _context.Expenses.RemoveRange(relatedExpenses);

            // Удаляем все записи в Recipes, связанные с данным лекарством
            var relatedRecipes = _context.Recipes.Where(r => r.Идентификатор_лекарства == id);
            _context.Recipes.RemoveRange(relatedRecipes);

            // Удаляем все записи в Orders, связанные с данным лекарством
            var relatedOrders = _context.Orders.Where(o => o.Идентификатор_лекарства == id);
            _context.Orders.RemoveRange(relatedOrders);

            // Удаляем лекарство
            _context.Drug.Remove(drug);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
