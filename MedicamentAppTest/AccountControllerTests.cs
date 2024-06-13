using MedicamentApp.Controllers;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MedicamentApp.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MedicamentAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new MedicamentAppContext(options))
            {
                // Add test data to in-memory database
                var user = new Users { Логин = "testuser", Пароль = "testpassword" };
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var controller = new AccountController(context);
                var model = new LoginViewModel { Login = "testuser", Password = "testpassword" };

                // Act
                var result = await controller.Login(model);

                // Assert
                Assert.IsType<RedirectToActionResult>(result);
            }
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MedicamentAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new MedicamentAppContext(options))
            {
                var controller = new AccountController(context);
                var model = new RegisterViewModel
                {
                    Login = "newuser",
                    Password = "newpassword",
                    ConfirmPassword = "newpassword",
                    RoleCode = 1,
                    AccountCode = 1
                };

                // Act
                var result = await controller.Register(model);

                // Assert
                Assert.IsType<RedirectToActionResult>(result);
            }
        }
    }
}
