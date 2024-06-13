using System.Collections.Generic;
using System.Linq;
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
    public class DeleteEmployeesControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new DeleteEmployeesController(GetInMemoryDbContext());

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var model = Assert.IsAssignableFrom<IEnumerable<DeleteEmployeesViewModel>>(viewResult.Model);
            Assert.Empty(model); // The list should be empty as there are no employees in the test database
        }

        [Fact]
        public async Task Delete_POST_RedirectsToHomeIndex_WhenEmployeeExists()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteEmployeesController(dbContext);
            var employee = new Employees { Идентификатор = 1, ФИО = "Employee 1", Должность = "Position 1", Код_роли = 1 };
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Delete(employee.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            var deletedEmployee = await dbContext.Employees.FindAsync(employee.Идентификатор);
            Assert.Null(deletedEmployee); // Employee should have been deleted from the database
        }

        [Fact]
        public async Task Delete_POST_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteEmployeesController(dbContext);

            // Act
            var result = await controller.Delete(1); // Assuming there is no employee with ID = 1

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        private MedicamentAppContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MedicamentAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new MedicamentAppContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
