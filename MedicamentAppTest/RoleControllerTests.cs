using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MedicamentApp.Controllers;
using MedicamentApp.DataContext;
using MedicamentApp.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.Tests
{
    public class RoleControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfRoles()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockRoleSet = new Mock<DbSet<Role>>();

            var roles = new List<Role>
            {
                new Role { Код_роли = 1, Название = "Admin" },
                new Role { Код_роли = 2, Название = "User" }
            }.AsQueryable();

            mockRoleSet.As<IQueryable<Role>>().Setup(m => m.Provider).Returns(roles.Provider);
            mockRoleSet.As<IQueryable<Role>>().Setup(m => m.Expression).Returns(roles.Expression);
            mockRoleSet.As<IQueryable<Role>>().Setup(m => m.ElementType).Returns(roles.ElementType);
            mockRoleSet.As<IQueryable<Role>>().Setup(m => m.GetEnumerator()).Returns(roles.GetEnumerator());

            mockContext.Setup(c => c.Role).Returns(mockRoleSet.Object);

            var controller = new RoleController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Role>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
