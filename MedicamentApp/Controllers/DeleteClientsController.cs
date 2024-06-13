using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteClientsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteClientsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteClients
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .Select(c => new DeleteClientsViewModel
                {
                    Идентификатор = c.Идентификатор,
                    ФИО = c.ФИО,
                    Дата_рождения = c.Дата_рождения,
                    Место_проживания = c.Место_проживания,
                    СНИЛС = c.СНИЛС,
                    Полис = c.Полис
                })
                .ToListAsync();

            return View(clients);
        }

        // POST: /DeleteClients/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
