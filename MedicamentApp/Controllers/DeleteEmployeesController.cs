using Microsoft.AspNetCore.Mvc;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Controllers
{
    public class DeleteEmployeesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DeleteEmployeesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /DeleteEmployees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Role)
                .Select(e => new DeleteEmployeesViewModel
                {
                    Идентификатор = e.Идентификатор,
                    ФИО = e.ФИО,
                    Должность = e.Должность,
                    Код_роли = e.Код_роли,
                    Название_роли = e.Role.Название // Предполагается, что у роли есть свойство Название
                })
                .ToListAsync();

            return View(employees);
        }

        // POST: /DeleteEmployees/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
