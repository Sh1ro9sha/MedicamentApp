using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddRecipesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddRecipesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddRecipes/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddRecipes/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddRecipesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipes
                {
                    Идентификатор = model.Идентификатор,
                    Дата_назначения = model.Дата_назначения,
                    Идентификатор_лекарства = model.Идентификатор_лекарства,
                    ФИО_лечащего_врача = model.ФИО_лечащего_врача,
                    Адрес_больницы = model.Адрес_больницы
                };
                    _context.Recipes.Add(recipe);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "MenuMain");
            }
            return View(model);
        }
    }
}
