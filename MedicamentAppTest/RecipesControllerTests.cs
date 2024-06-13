using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Controllers;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using Moq;
using Xunit;

namespace MedicamentApp.Tests
{
    public class RecipesControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfRecipes()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockRecipeSet = new Mock<DbSet<Recipes>>();

            var recipes = new List<Recipes>
            {
                new Recipes
                {
                    Идентификатор = 1,
                    Дата_назначения = DateTime.Now,
                    Идентификатор_лекарства = 1,
                    Drug = new Drug { Идентификатор = 1, Наименование = "Drug1" },
                    ФИО_лечащего_врача = "Dr. Smith",
                    Адрес_больницы = "123 Main St"
                },
                new Recipes
                {
                    Идентификатор = 2,
                    Дата_назначения = DateTime.Now,
                    Идентификатор_лекарства = 2,
                    Drug = new Drug { Идентификатор = 2, Наименование = "Drug2" },
                    ФИО_лечащего_врача = "Dr. Jones",
                    Адрес_больницы = "456 Elm St"
                }
            }.AsQueryable();

            mockRecipeSet.As<IQueryable<Recipes>>().Setup(m => m.Provider).Returns(recipes.Provider);
            mockRecipeSet.As<IQueryable<Recipes>>().Setup(m => m.Expression).Returns(recipes.Expression);
            mockRecipeSet.As<IQueryable<Recipes>>().Setup(m => m.ElementType).Returns(recipes.ElementType);
            mockRecipeSet.As<IQueryable<Recipes>>().Setup(m => m.GetEnumerator()).Returns(recipes.GetEnumerator());

            mockContext.Setup(c => c.Recipes).Returns(mockRecipeSet.Object);

            var controller = new RecipesController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Recipes>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
