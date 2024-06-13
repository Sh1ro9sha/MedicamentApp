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
    public class DrugControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfDrugs()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockDrugSet = new Mock<DbSet<Drug>>();

            var drugs = new List<Drug>
            {
                new Drug
                {
                    Идентификатор = 1,
                    Наименование = "Drug1",
                    Аннотация = "Annotation1",
                    Идентификатор_производителя = 1,
                    Manufacturers = new Manufacturers { Идентификатор = 1, Название = "Manufacturer1" },
                    Единица_измерения = "mg",
                    Место_хранения = "Storage1"
                },
                new Drug
                {
                    Идентификатор = 2,
                    Наименование = "Drug2",
                    Аннотация = "Annotation2",
                    Идентификатор_производителя = 2,
                    Manufacturers = new Manufacturers { Идентификатор = 2, Название = "Manufacturer2" },
                    Единица_измерения = "ml",
                    Место_хранения = "Storage2"
                }
            }.AsQueryable();

            mockDrugSet.As<IQueryable<Drug>>().Setup(m => m.Provider).Returns(drugs.Provider);
            mockDrugSet.As<IQueryable<Drug>>().Setup(m => m.Expression).Returns(drugs.Expression);
            mockDrugSet.As<IQueryable<Drug>>().Setup(m => m.ElementType).Returns(drugs.ElementType);
            mockDrugSet.As<IQueryable<Drug>>().Setup(m => m.GetEnumerator()).Returns(drugs.GetEnumerator());

            mockContext.Setup(c => c.Drug).Returns(mockDrugSet.Object);

            var controller = new DrugController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Drug>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
