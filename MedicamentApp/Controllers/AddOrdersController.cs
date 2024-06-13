using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddOrdersController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddOrdersController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddOrders/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // POST: /AddOrders/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrders(AddOrdersViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Создание нового товара
                var order = new Orders
                {
                    Идентификатор = model.Идентификатор,
                    Код_пользователя = model.Код_пользователя,
                    Идентификатор_лекарства = model.Идентификатор_лекарства,
                    Дата_заказа = model.Дата_заказа,
                    Количество = model.Количество,
                };

                // Добавление товара в базу данных
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "MenuMain"); // Перенаправляем на страницу меню после успешного добавления товара
            }
            // Возвращение представления Index в случае невалидных данных
            return View("Index", model);
        }
    }
}