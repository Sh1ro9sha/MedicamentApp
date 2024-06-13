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
    public class ManufacturersControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfManufacturers()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockManufacturersSet = new Mock<DbSet<Manufacturers>>();

            var manufacturers = new List<Manufacturers>
            {
                new Manufacturers
                {
                    Идентификатор = 1,
                    Название = "Manufacturer1",
                    Адрес = "123 Main St",
                    Контактный_телефон = "123-456-7890",
                    Drugs = new List<Drug>()
                },
                new Manufacturers
                {
                    Идентификатор = 2,
                    Название = "Manufacturer2",
                    Адрес = "456 Elm St",
                    Контактный_телефон = "098-765-4321",
                    Drugs = new List<Drug>()
                }
            }.AsQueryable();

            mockManufacturersSet.As<IQueryable<Manufacturers>>().Setup(m => m.Provider).Returns(manufacturers.Provider);
            mockManufacturersSet.As<IQueryable<Manufacturers>>().Setup(m => m.Expression).Returns(manufacturers.Expression);
            mockManufacturersSet.As<IQueryable<Manufacturers>>().Setup(m => m.ElementType).Returns(manufacturers.ElementType);
            mockManufacturersSet.As<IQueryable<Manufacturers>>().Setup(m => m.GetEnumerator()).Returns(manufacturers.GetEnumerator());

            mockContext.Setup(c => c.Manufacturers).Returns(mockManufacturersSet.Object);

            var controller = new ManufacturersController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Manufacturers>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
