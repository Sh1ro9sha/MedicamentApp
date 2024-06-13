using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly MedicamentAppContext _context;

        public ExpensesController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Expenses/Index
        [HttpGet]
        public IActionResult Index()
        {
            var expenses = _context.Expenses
                .ToList();
            return View(expenses);
        }
    }
}
