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
    public class AddManufacturersControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddManufacturersController(GetInMemoryDbContext());

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
            var controller = new AddManufacturersController(dbContext);
            var model = new AddManufacturersViewModel
            {
                Идентификатор = 1,
                Название = "Test Manufacturer",
                Адрес = "Test Address",
                Контактный_телефон = "1234567890"
            };

            // Act
            var result = await controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the manufacturer was added to the database
            var addedManufacturer = await dbContext.Manufacturers.FirstOrDefaultAsync(m => m.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedManufacturer);
            Assert.Equal(model.Название, addedManufacturer.Название);
            Assert.Equal(model.Адрес, addedManufacturer.Адрес);
            Assert.Equal(model.Контактный_телефон, addedManufacturer.Контактный_телефон);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddManufacturersController(GetInMemoryDbContext());
            var model = new AddManufacturersViewModel(); // This model will be invalid

            // Act
            var result = await controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddManufacturersViewModel>(viewResult.Model);
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
