using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly MedicamentAppContext _context;

        public RoleController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Role/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Role> roles = _context.Role.ToList();
            return View(roles);
        }
    }
}
