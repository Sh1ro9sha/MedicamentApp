using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public UsersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Users/Index
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Users> users = _context.Users.ToList();
            return View(users);
        }
    }
}
