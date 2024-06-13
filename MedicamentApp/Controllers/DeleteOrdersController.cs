using MedicamentApp.DataContext;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class DeleteOrdersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteOrdersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: DeleteOrders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Drug)
                .Select(o => new DeleteOrdersViewModel
                {
                    Идентификатор = o.Идентификатор,
                    Лекарство = o.Drug.Наименование,
                    Дата_заказа = o.Дата_заказа,
                    Количество = o.Количество
                })
                .ToListAsync();

            return View(orders);
        }

        // POST: DeleteOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
