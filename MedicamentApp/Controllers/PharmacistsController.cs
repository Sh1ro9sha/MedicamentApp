using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class PharmacistsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public PharmacistsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Pharmacists/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Pharmacists> pharmacists = _context.Pharmacists.ToList();
            return View(pharmacists);
        }
    }
}
