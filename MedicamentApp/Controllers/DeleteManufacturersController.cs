using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteManufacturersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteManufacturersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteManufacturers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var manufacturers = await _context.Manufacturers
                .Select(m => new DeleteManufacturersViewModel
                {
                    Идентификатор = m.Идентификатор,
                    Название = m.Название,
                    Адрес = m.Адрес,
                    Контактный_телефон = m.Контактный_телефон
                })
                .ToListAsync();

            return View(manufacturers);
        }

        // POST: /DeleteManufacturers/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            // Удаляем все записи в Drug, связанные с данным производителем
            var relatedDrugs = _context.Drug.Where(d => d.Идентификатор_производителя == id).ToList();

            foreach (var drug in relatedDrugs)
            {
                // Удаляем все записи в Profit, связанные с данным лекарством
                var relatedProfits = _context.Profit.Where(p => p.Идентификатор_лекарства == drug.Идентификатор);
                _context.Profit.RemoveRange(relatedProfits);

                // Удаляем все записи в Expenses, связанные с данным лекарством
                var relatedExpenses = _context.Expenses.Where(e => e.Идентификатор_лекарства == drug.Идентификатор);
                _context.Expenses.RemoveRange(relatedExpenses);

                // Удаляем все записи в Recipes, связанные с данным лекарством
                var relatedRecipes = _context.Recipes.Where(r => r.Идентификатор_лекарства == drug.Идентификатор);
                _context.Recipes.RemoveRange(relatedRecipes);

                // Удаляем все записи в Orders, связанные с данным лекарством
                var relatedOrders = _context.Orders.Where(o => o.Идентификатор_лекарства == drug.Идентификатор);
                _context.Orders.RemoveRange(relatedOrders);

                // Удаляем лекарство
                _context.Drug.Remove(drug);
            }

            // Удаляем производителя
            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
