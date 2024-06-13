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
    public class ProfitControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfProfit()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockProfitSet = new Mock<DbSet<Profit>>();

            var profits = new List<Profit>
            {
                new Profit
                {
                    Идентификатор = 1,
                    Идентификатор_лекарства = 1,
                    Drug = new Drug { Идентификатор = 1, Наименование = "Drug1" },
                    Дата_поступления = DateTime.Now,
                    Количество = 100,
                    Поставщик = "Supplier1",
                    Цена_закупки = 50.0m
                },
                new Profit
                {
                    Идентификатор = 2,
                    Идентификатор_лекарства = 2,
                    Drug = new Drug { Идентификатор = 2, Наименование = "Drug2" },
                    Дата_поступления = DateTime.Now,
                    Количество = 200,
                    Поставщик = "Supplier2",
                    Цена_закупки = 75.0m
                }
            }.AsQueryable();

            mockProfitSet.As<IQueryable<Profit>>().Setup(m => m.Provider).Returns(profits.Provider);
            mockProfitSet.As<IQueryable<Profit>>().Setup(m => m.Expression).Returns(profits.Expression);
            mockProfitSet.As<IQueryable<Profit>>().Setup(m => m.ElementType).Returns(profits.ElementType);
            mockProfitSet.As<IQueryable<Profit>>().Setup(m => m.GetEnumerator()).Returns(profits.GetEnumerator());

            mockContext.Setup(c => c.Profit).Returns(mockProfitSet.Object);

            var controller = new ProfitController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Profit>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
