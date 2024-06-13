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
    public class AddClientsControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddClientsController(GetInMemoryDbContext());

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
            var controller = new AddClientsController(dbContext);
            var model = new AddClientsViewModel
            {
                Идентификатор = 1,
                ФИО = "John Doe",
                Дата_рождения = new DateTime(1990, 1, 1),
                Место_проживания = "123 Main St",
                СНИЛС = "123-456-789 00",
                Полис = "1234567890"
            };

            // Act
            var result = await controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the client was added to the database
            var addedClient = await dbContext.Clients.FirstOrDefaultAsync(c => c.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedClient);
            Assert.Equal(model.ФИО, addedClient.ФИО);
            Assert.Equal(model.Дата_рождения, addedClient.Дата_рождения);
            Assert.Equal(model.Место_проживания, addedClient.Место_проживания);
            Assert.Equal(model.СНИЛС, addedClient.СНИЛС);
            Assert.Equal(model.Полис, addedClient.Полис);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddClientsController(GetInMemoryDbContext());
            var model = new AddClientsViewModel(); // This model will be invalid

            // Act
            var result = await controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddClientsViewModel>(viewResult.Model);
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
