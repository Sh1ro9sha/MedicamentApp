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
    public class OrdersControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfOrders()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockOrderSet = new Mock<DbSet<Orders>>();

            var orders = new List<Orders>
            {
                new Orders
                {
                    Идентификатор = 1,
                    Код_пользователя = 1,
                    User = new Users { Код_пользователя = 1, Логин = "user1", Пароль = "password1", Код_роли = 1 },
                    Идентификатор_лекарства = 1,
                    Drug = new Drug { Идентификатор = 1, Наименование = "Drug1" },
                    Дата_заказа = DateTime.Now,
                    Количество = 10
                },
                new Orders
                {
                    Идентификатор = 2,
                    Код_пользователя = 2,
                    User = new Users { Код_пользователя = 2, Логин = "user2", Пароль = "password2", Код_роли = 2 },
                    Идентификатор_лекарства = 2,
                    Drug = new Drug { Идентификатор = 2, Наименование = "Drug2" },
                    Дата_заказа = DateTime.Now,
                    Количество = 20
                }
            }.AsQueryable();

            mockOrderSet.As<IQueryable<Orders>>().Setup(m => m.Provider).Returns(orders.Provider);
            mockOrderSet.As<IQueryable<Orders>>().Setup(m => m.Expression).Returns(orders.Expression);
            mockOrderSet.As<IQueryable<Orders>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            mockOrderSet.As<IQueryable<Orders>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            mockContext.Setup(c => c.Orders).Returns(mockOrderSet.Object);

            var controller = new OrdersController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Orders>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
