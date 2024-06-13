using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicamentApp.Controllers;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using Moq;
using Xunit;

namespace MedicamentApp.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfEmployees()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockEmployeesSet = new Mock<DbSet<Employees>>();

            var employees = new List<Employees>
            {
                new Employees { Идентификатор = 1, ФИО = "John Doe", Должность = "Manager", Код_роли = 1 },
                new Employees { Идентификатор = 2, ФИО = "Jane Smith", Должность = "Assistant", Код_роли = 2 }
            }.AsQueryable();

            mockEmployeesSet.As<IQueryable<Employees>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockEmployeesSet.As<IQueryable<Employees>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockEmployeesSet.As<IQueryable<Employees>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockEmployeesSet.As<IQueryable<Employees>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockContext.Setup(c => c.Employees).Returns(mockEmployeesSet.Object);

            var controller = new EmployeesController(mockContext.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employees>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var controller = new EmployeesController(mockContext.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var controller = new EmployeesController(mockContext.Object);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
