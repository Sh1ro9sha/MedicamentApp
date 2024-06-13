using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.DataContext;
using MedicamentApp.Models;

namespace MedicamentApp.Controllers
{
    public class Recipes1Controller : Controller
    {
        private readonly MedicamentAppContext _context;

        public Recipes1Controller(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: Recipes1
        public async Task<IActionResult> Index()
        {
            var medicamentAppContext = _context.Recipes.Include(r => r.Drug);
            return View(await medicamentAppContext.ToListAsync());
        }

        // GET: Recipes1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes
                .Include(r => r.Drug)
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (recipes == null)
            {
                return NotFound();
            }

            return View(recipes);
        }

        // GET: Recipes1/Create
        public IActionResult Create()
        {
            ViewData["Идентификатор_лекарства"] = new SelectList(_context.Drug, "Идентификатор", "Наименование");
            return View();
        }

        // POST: Recipes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Идентификатор,Дата_назначения,Идентификатор_лекарства,ФИО_лечащего_врача,Адрес_больницы")] Recipes recipes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Идентификатор_лекарства"] = new SelectList(_context.Drug, "Идентификатор", "Наименование", recipes.Идентификатор_лекарства);
            return View(recipes);
        }

        // GET: Recipes1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes.FindAsync(id);
            if (recipes == null)
            {
                return NotFound();
            }
            ViewData["Идентификатор_лекарства"] = new SelectList(_context.Drug, "Идентификатор", "Наименование", recipes.Идентификатор_лекарства);
            return View(recipes);
        }

        // POST: Recipes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Идентификатор,Дата_назначения,Идентификатор_лекарства,ФИО_лечащего_врача,Адрес_больницы")] Recipes recipes)
        {
            if (id != recipes.Идентификатор)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipesExists(recipes.Идентификатор))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Идентификатор_лекарства"] = new SelectList(_context.Drug, "Идентификатор", "Наименование", recipes.Идентификатор_лекарства);
            return View(recipes);
        }

        // GET: Recipes1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes
                .Include(r => r.Drug)
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (recipes == null)
            {
                return NotFound();
            }

            return View(recipes);
        }

        // POST: Recipes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipes = await _context.Recipes.FindAsync(id);
            if (recipes != null)
            {
                _context.Recipes.Remove(recipes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipesExists(int id)
        {
            return _context.Recipes.Any(e => e.Идентификатор == id);
        }
    }
}
