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
    public class AddProfitControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddProfitController(GetInMemoryDbContext());

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
            var controller = new AddProfitController(dbContext);
            var model = new AddProfitViewModel
            {
                Идентификатор = 1,
                Идентификатор_лекарства = 1,
                Дата_поступления = DateTime.Now.Date,
                Количество = 10,
                Поставщик = "Supplier",
                Цена_закупки = 50.00m
            };

            // Act
            var result = await controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the profit was added to the database
            var addedProfit = await dbContext.Profit.FirstOrDefaultAsync(p => p.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedProfit);
            Assert.Equal(model.Идентификатор_лекарства, addedProfit.Идентификатор_лекарства);
            Assert.Equal(model.Дата_поступления, addedProfit.Дата_поступления);
            Assert.Equal(model.Количество, addedProfit.Количество);
            Assert.Equal(model.Поставщик, addedProfit.Поставщик);
            Assert.Equal(model.Цена_закупки, addedProfit.Цена_закупки);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddProfitController(GetInMemoryDbContext());
            var model = new AddProfitViewModel(); // This model will be invalid

            // Act
            var result = await controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddProfitViewModel>(viewResult.Model);
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
