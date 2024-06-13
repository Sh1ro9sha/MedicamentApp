using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteProfitController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteProfitController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteProfit
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profits = await _context.Profit
                .Include(p => p.Drug)
                .Select(p => new DeleteProfitViewModel
                {
                    Идентификатор = p.Идентификатор,
                    Идентификатор_лекарства = p.Идентификатор_лекарства,
                    Наименование_лекарства = p.Drug.Наименование,
                    Дата_поступления = p.Дата_поступления,
                    Количество = p.Количество,
                    Поставщик = p.Поставщик,
                    Цена_закупки = p.Цена_закупки
                })
                .ToListAsync();

            return View(profits);
        }

        // POST: /DeleteProfit/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profit = await _context.Profit.FindAsync(id);

            if (profit == null)
            {
                return NotFound();
            }

            _context.Profit.Remove(profit);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
