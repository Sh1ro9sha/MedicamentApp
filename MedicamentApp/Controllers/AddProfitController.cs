using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddProfitController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddProfitController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddProfit/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddProfit/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddProfitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profit = new Profit
                {
                    Идентификатор = model.Идентификатор,
                    Идентификатор_лекарства = model.Идентификатор_лекарства,
                    Дата_поступления = model.Дата_поступления,
                    Количество = model.Количество,
                    Поставщик = model.Поставщик,
                    Цена_закупки = model.Цена_закупки
                };

                    _context.Profit.Add(profit);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "MenuMain");
                
            }
            return View(model);
        }
    }
}
