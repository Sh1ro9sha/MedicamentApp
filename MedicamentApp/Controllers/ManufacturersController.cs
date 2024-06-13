using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public ManufacturersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Manufacturers/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Manufacturers> manufacturers = _context.Manufacturers.ToList();
            return View(manufacturers);
        }
    }
}
