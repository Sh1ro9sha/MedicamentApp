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
    public class Drug1Controller : Controller
    {
        private readonly MedicamentAppContext _context;

        public Drug1Controller(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: Drug1
        public async Task<IActionResult> Index()
        {
            var medicamentAppContext = _context.Drug.Include(d => d.Manufacturers);
            return View(await medicamentAppContext.ToListAsync());
        }

        // GET: Drug1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug
                .Include(d => d.Manufacturers)
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // GET: Drug1/Create
        public IActionResult Create()
        {
            ViewData["Идентификатор_производителя"] = new SelectList(_context.Manufacturers, "Идентификатор", "Название");
            return View();
        }

        // POST: Drug1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Идентификатор,Наименование,Аннотация,Идентификатор_производителя,Единица_измерения,Место_хранения")] Drug drug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Идентификатор_производителя"] = new SelectList(_context.Manufacturers, "Идентификатор", "Название", drug.Идентификатор_производителя);
            return View(drug);
        }

        // GET: Drug1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }
            ViewData["Идентификатор_производителя"] = new SelectList(_context.Manufacturers, "Идентификатор", "Название", drug.Идентификатор_производителя);
            return View(drug);
        }

        // POST: Drug1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Идентификатор,Наименование,Аннотация,Идентификатор_производителя,Единица_измерения,Место_хранения")] Drug drug)
        {
            if (id != drug.Идентификатор)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugExists(drug.Идентификатор))
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
            ViewData["Идентификатор_производителя"] = new SelectList(_context.Manufacturers, "Идентификатор", "Название", drug.Идентификатор_производителя);
            return View(drug);
        }

        // GET: Drug1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug
                .Include(d => d.Manufacturers)
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // POST: Drug1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drug = await _context.Drug.FindAsync(id);
            if (drug != null)
            {
                _context.Drug.Remove(drug);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugExists(int id)
        {
            return _context.Drug.Any(e => e.Идентификатор == id);
        }
    }
}
