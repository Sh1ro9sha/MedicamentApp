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
    public class AddDrugControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddDrugController(GetInMemoryDbContext());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task AddDrug_POST_RedirectsToMenuMain_WhenModelStateIsValid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new AddDrugController(dbContext);
            var model = new AddDrugViewModel
            {
                Идентификатор = 1,
                Наименование = "Test Drug",
                Аннотация = "Test Annotation",
                Идентификатор_производителя = 1,
                Единица_измерения = "Test Unit",
                Место_хранения = "Test Location"
            };

            // Act
            var result = await controller.AddDrug(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the drug was added to the database
            var addedDrug = await dbContext.Drug.FirstOrDefaultAsync(d => d.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedDrug);
            Assert.Equal(model.Наименование, addedDrug.Наименование);
            Assert.Equal(model.Аннотация, addedDrug.Аннотация);
            Assert.Equal(model.Идентификатор_производителя, addedDrug.Идентификатор_производителя);
            Assert.Equal(model.Единица_измерения, addedDrug.Единица_измерения);
            Assert.Equal(model.Место_хранения, addedDrug.Место_хранения);
        }

        [Fact]
        public async Task AddDrug_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddDrugController(GetInMemoryDbContext());
            var model = new AddDrugViewModel(); // This model will be invalid

            // Act
            var result = await controller.AddDrug(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddDrugViewModel>(viewResult.Model);
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
