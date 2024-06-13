﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Models;
using MedicamentApp.DataContext;
using MedicamentApp.ViewModels;

namespace MedicamentApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MedicamentAppContext _context;

        public AccountController(MedicamentAppContext context)
        {
            _context = context;
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }
        // GET: /Account/RegistrationSuccess
        public IActionResult RegistrationSuccess()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Проверяем наличие пользователя в базе данных по логину и паролю
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Логин == model.Login && u.Пароль == model.Password);

                if (user != null)
                {
                    // Проверяем, существует ли роль пользователя
                    if (user.Role != null)
                    {
                        // Определяем код роли пользователя
                        var roleCode = user.Role.Код_роли;

                        // Перенаправляем на соответствующую страницу в зависимости от роли
                        if (roleCode == 1)
                        {
                            // Роль с индексом 1 - перенаправление на страницу меню студента
                            return RedirectToAction("Index", "MenuMain");
                        }
                        else if (roleCode == 2)
                        {
                            // Роль с индексом 2 - перенаправление на страницу меню учителя
                            return RedirectToAction("Index", "MenuPharmacists");
                        }
                        else if (roleCode == 3)
                        {
                            // Роль с индексом 3 - перенаправление на страницу меню админа
                            return RedirectToAction("Index", "MenuClient");
                        }
                    }
                    else
                    {
                        // Если роль пользователя не определена, добавляем сообщение об ошибке в ModelState
                        ModelState.AddModelError(string.Empty, "Роль пользователя не определена");
                    }
                }
                else
                {
                    // Если пользователь не найден, добавляем сообщение об ошибке в ModelState
                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
            }
            // Если ModelState невалиден или произошла ошибка при проверке, возвращаем представление с моделью для исправления ошибок
            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Проверка на уникальность логина
                if (await _context.Users.AnyAsync(u => u.Логин == model.Login))
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
                    return View(model);
                }

                // Проверка на уникальность кода роли
                if (await _context.Role.AllAsync(r => r.Код_роли != model.RoleCode))
                {
                    ModelState.AddModelError("RoleCode", "Роль с указанным кодом не существует");
                    return View(model);
                }

                // Создание нового аккаунта
                var users = new Users
                {
                    Логин = model.Login,
                    Пароль = model.Password,
                    Код_пользователя = model.AccountCode
                };

                // Установка роли для аккаунта
                users.Role = await _context.Role.FindAsync(model.RoleCode);

                // Сохранение аккаунта в базе данных
                _context.Add(users);
                await _context.SaveChangesAsync();

                // Проверка роли и перенаправление на соответствующую страницу
                if (model.RoleCode == 1)
                {
                    return RedirectToAction("Index", "MenuMain");
                }
                else if (model.RoleCode == 2)
                {
                    return RedirectToAction("Index", "MenuPharmacists");
                }
                else if (model.RoleCode == 3)
                {
                    return RedirectToAction("Index", "MenuClient");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            // Если ModelState невалиден, возвращаем представление с моделью для исправления ошибок
            return View(model);
        }
    }
}