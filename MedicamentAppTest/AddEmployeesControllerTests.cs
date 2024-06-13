using System;
using System.Threading.Tasks;
using MedicamentApp.Controllers;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using MedicamentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MedicamentApp.Tests
{
    public class AddEmployeesControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddEmployeesController(GetInMemoryDbContext());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task AddEmployees_POST_RedirectsToMenuMain_WhenModelStateIsValid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new AddEmployeesController(dbContext);
            var model = new AddEmployeesViewModel
            {
                Идентификатор = 1,
                ФИО = "John Doe",
                Должность = "Manager",
                Код_роли = 1
            };

            // Act
            var result = await controller.AddEmployees(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the employee was added to the database
            var addedEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedEmployee);
            Assert.Equal(model.ФИО, addedEmployee.ФИО);
            Assert.Equal(model.Должность, addedEmployee.Должность);
            Assert.Equal(model.Код_роли, addedEmployee.Код_роли);
        }

        [Fact]
        public async Task AddEmployees_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddEmployeesController(GetInMemoryDbContext());
            var model = new AddEmployeesViewModel(); // This model will be invalid

            // Act
            var result = await controller.AddEmployees(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddEmployeesViewModel>(viewResult.Model);
        }

        private MedicamentAppContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MedicamentAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new MedicamentAppContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
