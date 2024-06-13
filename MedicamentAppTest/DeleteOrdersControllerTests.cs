using System;
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
    public class DeleteOrdersControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteOrdersController(dbContext);
            var order = new Orders { Идентификатор = 1, Дата_заказа = DateTime.Now, Количество = 10 };
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<DeleteOrdersViewModel>>(viewResult.ViewData.Model);
            var ordersList = model.ToList();
            Assert.Single(ordersList); // Assuming we have only one order in the test database
            Assert.Equal(order.Идентификатор, ordersList[0].Идентификатор);
            Assert.Equal(order.Дата_заказа, ordersList[0].Дата_заказа);
            Assert.Equal(order.Количество, ordersList[0].Количество);
        }

        [Fact]
        public async Task DeleteConfirmed_POST_DeletesOrderAndRedirectsToIndex()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteOrdersController(dbContext);
            var order = new Orders { Идентификатор = 1, Дата_заказа = DateTime.Now, Количество = 10 };
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(order.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);

            var deletedOrder = await dbContext.Orders.FindAsync(order.Идентификатор);
            Assert.Null(deletedOrder); // Order should have been deleted from the database
        }

        [Fact]
        public async Task DeleteConfirmed_POST_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteOrdersController(dbContext);

            // Act
            var result = await controller.DeleteConfirmed(1); // Assuming there is no order with ID = 1

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
