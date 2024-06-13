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
    public class AddExpensesControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddExpensesController(GetInMemoryDbContext());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task Index_POST_RedirectsToMenuMain_WhenModelStateIsValid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new AddExpensesController(dbContext);
            var model = new AddExpensesViewModel
            {
                Идентификатор = 1,
                Идентификатор_лекарства = 1,
                Дата_реализации = DateTime.Now,
                Количество = 10,
                Отпускная_цена = 50.00m
            };

            // Act
            var result = await controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the expense was added to the database
            var addedExpense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedExpense);
            Assert.Equal(model.Идентификатор_лекарства, addedExpense.Идентификатор_лекарства);
            Assert.Equal(model.Дата_реализации, addedExpense.Дата_реализации);
            Assert.Equal(model.Количество, addedExpense.Количество);
            Assert.Equal(model.Отпускная_цена, addedExpense.Отпускная_цена);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddExpensesController(GetInMemoryDbContext());
            var model = new AddExpensesViewModel(); // This model will be invalid

            // Act
            var result = await controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddExpensesViewModel>(viewResult.Model);
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
