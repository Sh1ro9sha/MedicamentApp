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
    public class DeleteDrugControllerTests
    {
        [Fact]
        public async Task Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new DeleteDrugController(GetInMemoryDbContext());

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            var model = Assert.IsAssignableFrom<IEnumerable<DeleteDrugViewModel>>(viewResult.Model);
            Assert.Empty(model); // The list should be empty as there are no drugs in the test database
        }

        [Fact]
        public async Task Delete_POST_RedirectsToHomeIndex_WhenDrugExists()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteDrugController(dbContext);
            var drug = new Drug { Идентификатор = 1, Наименование = "Drug 1", Идентификатор_производителя = 1 };
            dbContext.Drug.Add(drug);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.Delete(drug.Идентификатор);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);

            var deletedDrug = await dbContext.Drug.FindAsync(drug.Идентификатор);
            Assert.Null(deletedDrug); // Drug should have been deleted from the database
        }

        [Fact]
        public async Task Delete_POST_ReturnsNotFound_WhenDrugDoesNotExist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new DeleteDrugController(dbContext);

            // Act
            var result = await controller.Delete(1); // Assuming there is no drug with ID = 1

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
