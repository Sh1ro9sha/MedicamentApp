using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;

namespace MedicamentApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public OrdersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /Orders/Index
        [HttpGet]
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.User) // Загрузка связанных данных о пользователях
                .ToList();
            return View(orders);
        }
    }
}
