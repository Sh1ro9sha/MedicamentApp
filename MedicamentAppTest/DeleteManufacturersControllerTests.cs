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
    public class DeleteManufacturersControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new DeleteManufacturersController(GetInMemoryDbContext());

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var model = Assert.IsAssignableFrom<IEnumerable<DeleteManufacturersViewModel>>(viewResult.Model);
            Assert.Empty(model); // The list should be empty as there are no manufacturers in the test database
        }

        [Fact]
        public async Task Delete_POST_RedirectsToHomeIndex_WhenManufacturerExists()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteManufacturersController(dbContext);
            var manufacturer = new Manufacturers { Идентификатор = 1, Название = "Manufacturer 1", Адрес = "Address 1", Контактный_телефон = "123456789" };
            dbContext.Manufacturers.Add(manufacturer);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Delete(manufacturer.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            var deletedManufacturer = await dbContext.Manufacturers.FindAsync(manufacturer.Идентификатор);
            Assert.Null(deletedManufacturer); // Manufacturer should have been deleted from the database
        }

        [Fact]
        public async Task Delete_POST_ReturnsNotFound_WhenManufacturerDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteManufacturersController(dbContext);

            // Act
            var result = await controller.Delete(1); // Assuming there is no manufacturer with ID = 1

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
