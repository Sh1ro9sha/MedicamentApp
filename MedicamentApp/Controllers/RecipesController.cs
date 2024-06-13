using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public RecipesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Recipes/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Recipes> recipes = _context.Recipes.ToList();
            return View(recipes);
        }
    }
}
