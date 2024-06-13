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
    public class DeleteExpensesControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new DeleteExpensesController(GetInMemoryDbContext());

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var model = Assert.IsAssignableFrom<IEnumerable<DeleteExpensesViewModel>>(viewResult.Model);
            Assert.Empty(model); // The list should be empty as there are no expenses in the test database
        }

        [Fact]
        public async Task Delete_POST_RedirectsToHomeIndex_WhenExpenseExists()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteExpensesController(dbContext);
            var expense = new Expenses { Идентификатор = 1, Идентификатор_лекарства = 1, Дата_реализации = DateTime.Now, Количество = 1, Отпускная_цена = 10 };
            dbContext.Expenses.Add(expense);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Delete(expense.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            var deletedExpense = await dbContext.Expenses.FindAsync(expense.Идентификатор);
            Assert.Null(deletedExpense); // Expense should have been deleted from the database
        }

        [Fact]
        public async Task Delete_POST_ReturnsNotFound_WhenExpenseDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteExpensesController(dbContext);

            // Act
            var result = await controller.Delete(1); // Assuming there is no expense with ID = 1

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
