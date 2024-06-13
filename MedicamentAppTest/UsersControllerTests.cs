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
    public class UsersControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfUsers()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockUserSet = new Mock<DbSet<Users>>();

            var users = new List<Users>
            {
                new Users { Код_пользователя = 1, Логин = "user1", Пароль = "password1", Код_роли = 1 },
                new Users { Код_пользователя = 2, Логин = "user2", Пароль = "password2", Код_роли = 2 }
            }.AsQueryable();

            mockUserSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);

            var controller = new UsersController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Users>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
