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
    public class AddPharmacistsControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddPharmacistsController(GetInMemoryDbContext());

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public async Task AddPharmacists_POST_RedirectsToMenuMain_WhenModelStateIsValid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new AddPharmacistsController(dbContext);
            var model = new AddPharmacistsViewModel
            {
                Идентификатор = 1,
                ФИО = "John Doe",
                Дата_приема = DateTime.Now.Date,
                Статус = "Active"
            };

            // Act
            var result = await controller.AddPharmacists(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the pharmacist was added to the database
            var addedPharmacist = await dbContext.Pharmacists.FirstOrDefaultAsync(p => p.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedPharmacist);
            Assert.Equal(model.ФИО, addedPharmacist.ФИО);
            Assert.Equal(model.Дата_приема, addedPharmacist.Дата_приема);
            Assert.Equal(model.Статус, addedPharmacist.Статус);
        }

        [Fact]
        public async Task AddPharmacists_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddPharmacistsController(GetInMemoryDbContext());
            var model = new AddPharmacistsViewModel(); // This model will be invalid

            // Act
            var result = await controller.AddPharmacists(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddPharmacistsViewModel>(viewResult.Model);
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
