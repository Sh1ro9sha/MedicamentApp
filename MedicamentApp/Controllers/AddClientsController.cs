using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddClientsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddClientsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddClients/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddClients/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddClientsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Clients
                {
                    Идентификатор = model.Идентификатор,
                    ФИО = model.ФИО,
                    Дата_рождения = model.Дата_рождения,
                    Место_проживания = model.Место_проживания,
                    СНИЛС = model.СНИЛС,
                    Полис = model.Полис
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "MenuMain");
            }
            return View(model);
        }
    }
}
