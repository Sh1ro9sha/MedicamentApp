using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MedicamentApp.Controllers
{
    public class AddDrugController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AddDrugController(MedicamentAppContext context)
        {
            _context = context;
        }

        // GET: /AddDrug/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDrug(AddDrugViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Создание нового товара
                var drug = new Drug
                {
                    Идентификатор = model.Идентификатор,
                    Наименование = model.Наименование,
                    Аннотация = model.Аннотация,
                    Идентификатор_производителя = model.Идентификатор_производителя,
                    Единица_измерения = model.Единица_измерения,
                    Место_хранения = model.Место_хранения
                };

                // Добавление товара в базу данных
                _context.Drug.Add(drug);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "MenuMain"); // Перенаправляем на страницу меню после успешного добавления товара
            }
            // Возвращение представления Index в случае невалидных данных
            return View("Index", model);
        }
    }
}