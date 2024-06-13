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
    public class Pharmacists1Controller : Controller
    {
        private readonly MedicamentAppContext _context;

        public Pharmacists1Controller(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: Pharmacists1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pharmacists.ToListAsync());
        }

        // GET: Pharmacists1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacists = await _context.Pharmacists
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (pharmacists == null)
            {
                return NotFound();
            }

            return View(pharmacists);
        }

        // GET: Pharmacists1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pharmacists1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Идентификатор,ФИО,Дата_приема,Статус")] Pharmacists pharmacists)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacists);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacists);
        }

        // GET: Pharmacists1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacists = await _context.Pharmacists.FindAsync(id);
            if (pharmacists == null)
            {
                return NotFound();
            }
            return View(pharmacists);
        }

        // POST: Pharmacists1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Идентификатор,ФИО,Дата_приема,Статус")] Pharmacists pharmacists)
        {
            if (id != pharmacists.Идентификатор)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistsExists(pharmacists.Идентификатор))
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
            return View(pharmacists);
        }

        // GET: Pharmacists1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacists = await _context.Pharmacists
                .FirstOrDefaultAsync(m => m.Идентификатор == id);
            if (pharmacists == null)
            {
                return NotFound();
            }

            return View(pharmacists);
        }

        // POST: Pharmacists1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacists = await _context.Pharmacists.FindAsync(id);
            if (pharmacists != null)
            {
                _context.Pharmacists.Remove(pharmacists);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacistsExists(int id)
        {
            return _context.Pharmacists.Any(e => e.Идентификатор == id);
        }
    }
}
