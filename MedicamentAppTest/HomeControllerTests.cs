using System;
using MedicamentApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MedicamentApp.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
