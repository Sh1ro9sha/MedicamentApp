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
    public class ClientsControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfClients()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockClientsSet = new Mock<DbSet<Clients>>();

            var clients = new List<Clients>
            {
                new Clients
                {
                    Идентификатор = 1,
                    ФИО = "John Doe",
                    Дата_рождения = new DateTime(1990, 1, 1),
                    Место_проживания = "123 Main St",
                    СНИЛС = "123-456-789 00",
                    Полис = "1234567890"
                },
                new Clients
                {
                    Идентификатор = 2,
                    ФИО = "Jane Smith",
                    Дата_рождения = new DateTime(1985, 5, 5),
                    Место_проживания = "456 Elm St",
                    СНИЛС = "987-654-321 00",
                    Полис = "0987654321"
                }
            }.AsQueryable();

            mockClientsSet.As<IQueryable<Clients>>().Setup(m => m.Provider).Returns(clients.Provider);
            mockClientsSet.As<IQueryable<Clients>>().Setup(m => m.Expression).Returns(clients.Expression);
            mockClientsSet.As<IQueryable<Clients>>().Setup(m => m.ElementType).Returns(clients.ElementType);
            mockClientsSet.As<IQueryable<Clients>>().Setup(m => m.GetEnumerator()).Returns(clients.GetEnumerator());

            mockContext.Setup(c => c.Clients).Returns(mockClientsSet.Object);

            var controller = new ClientsController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Clients>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
