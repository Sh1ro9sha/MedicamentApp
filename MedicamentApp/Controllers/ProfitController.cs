using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class ProfitController : Controller
    {
        private readonly MedicamentAppContext _context;

        public ProfitController(MedicamentAppContext context)
        {
            _context = context;
        }
        [Route("Profit")]

        // GET: /Profit/Index
        [HttpGet]
        public IActionResult Index()
        {
            var profit = _context.Profit
                .ToList();
            return View(profit);
        }
    }
}

