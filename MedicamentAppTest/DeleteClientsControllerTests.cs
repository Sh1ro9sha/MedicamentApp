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
    public class DeleteClientsControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new DeleteClientsController(GetInMemoryDbContext());

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var model = Assert.IsAssignableFrom<IEnumerable<DeleteClientsViewModel>>(viewResult.Model);
            Assert.Empty(model); // The list should be empty as there are no clients in the test database
        }

        [Fact]
        public async Task Delete_POST_RedirectsToHomeIndex_WhenClientExists()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteClientsController(dbContext);
            var client = new Clients { Идентификатор = 1, ФИО = "John Doe", Дата_рождения = DateTime.Now.Date };
            dbContext.Clients.Add(client);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Delete(client.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            var deletedClient = await dbContext.Clients.FindAsync(client.Идентификатор);
            Assert.Null(deletedClient); // Client should have been deleted from the database
        }

        [Fact]
        public async Task Delete_POST_ReturnsNotFound_WhenClientDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteClientsController(dbContext);

            // Act
            var result = await controller.Delete(1); // Assuming there is no client with ID = 1

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
