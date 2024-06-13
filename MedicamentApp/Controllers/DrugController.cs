using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class DrugController : Controller
    {
        private readonly MedicamentAppContext _context;

        public DrugController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Drug/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Drug> drugs = _context.Drug.ToList();
            return View(drugs);
        }
    }
}
