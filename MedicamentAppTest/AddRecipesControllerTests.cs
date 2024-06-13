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
    public class AddRecipesControllerTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new AddRecipesController(GetInMemoryDbContext());

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
            var controller = new AddRecipesController(dbContext);
            var model = new AddRecipesViewModel
            {
                Идентификатор = 1,
                Дата_назначения = DateTime.Now.Date,
                Идентификатор_лекарства = 1,
                ФИО_лечащего_врача = "Doctor",
                Адрес_больницы = "Hospital"
            };

            // Act
            var result = await controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("MenuMain", redirectToActionResult.ControllerName);

            // Verify if the recipe was added to the database
            var addedRecipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Идентификатор == model.Идентификатор);
            Assert.NotNull(addedRecipe);
            Assert.Equal(model.Дата_назначения, addedRecipe.Дата_назначения);
            Assert.Equal(model.Идентификатор_лекарства, addedRecipe.Идентификатор_лекарства);
            Assert.Equal(model.ФИО_лечащего_врача, addedRecipe.ФИО_лечащего_врача);
            Assert.Equal(model.Адрес_больницы, addedRecipe.Адрес_больницы);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AddRecipesController(GetInMemoryDbContext());
            var model = new AddRecipesViewModel(); // This model will be invalid

            // Act
            var result = await controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            Assert.IsType<AddRecipesViewModel>(viewResult.Model);
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
