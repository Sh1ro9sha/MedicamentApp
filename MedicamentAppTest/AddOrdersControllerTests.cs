// Install these NuGet packages in your test project:
// xunit
// xunit.runner.visualstudio
// Moq

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
    public class AddOrdersControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddOrdersController(GetInMemoryDbContext());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task AddOrders_POST_RedirectsToMenuMain_WhenModelStateIsValid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new AddOrdersController(dbContext);
            var model = new AddOrdersViewModel
            {
                Идентификатор = 1,
                Код_пользователя = 1,
                Идентификатор_лекарства = 1,
                Дата_заказа = DateTime.Now,
                Количество = 10
            };

            // Act
            var result = await controller.AddOrders(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the order was added to the database
            var addedOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedOrder);
            Assert.Equal(model.Код_пользователя, addedOrder.Код_пользователя);
            Assert.Equal(model.Идентификатор_лекарства, addedOrder.Идентификатор_лекарства);
            Assert.Equal(model.Дата_заказа, addedOrder.Дата_заказа);
            Assert.Equal(model.Количество, addedOrder.Количество);
        }

        [Fact]
        public async Task AddOrders_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddOrdersController(GetInMemoryDbContext());
            var model = new AddOrdersViewModel(); // This model will be invalid

            // Act
            var result = await controller.AddOrders(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddOrdersViewModel>(viewResult.Model);
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
