using MedicamentApp.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace MedicamentApp.Controllers
{
    public class HomeController : Controller
    {
        // Действие для отображения главной страницы
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}