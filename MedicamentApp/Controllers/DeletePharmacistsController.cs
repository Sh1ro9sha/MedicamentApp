using MedicamentApp.DataContext;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class DeletePharmacistsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeletePharmacistsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: DeletePharmacists
        public async Task<IActionResult> Index()
        {
            var pharmacists = await _context.Pharmacists
                .Select(p => new DeletePharmacistsViewModel
                {
                    Идентификатор = p.Идентификатор,
                    ФИО = p.ФИО,
                    Дата_приема = p.Дата_приема.ToString("dd.MM.yyyy"),
                    Статус = p.Статус
                })
                .ToListAsync();

            return View(pharmacists);
        }

        // POST: DeletePharmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            _context.Pharmacists.Remove(pharmacist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
