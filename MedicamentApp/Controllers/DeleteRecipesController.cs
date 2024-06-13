using MedicamentApp.DataContext;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class DeleteRecipesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteRecipesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: DeleteRecipes
        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Drug)
                .Select(r => new DeleteRecipesViewModel
                {
                    Идентификатор = r.Идентификатор,
                    Дата_назначения = r.Дата_назначения.ToString("dd.MM.yyyy"),
                    Наименование_лекарства = r.Drug.Наименование,
                    ФИО_лечащего_врача = r.ФИО_лечащего_врача,
                    Адрес_больницы = r.Адрес_больницы
                })
                .ToListAsync();

            return View(recipes);
        }

        // POST: DeleteRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
