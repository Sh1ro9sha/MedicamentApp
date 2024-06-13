using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly MedicamentAppContext _context;

        public ClientsController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Clients/Index
        [HttpGet]
        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }
    }
}
