using Microsoft.AspNetCore.Mvc;
using MedicamentApp.Controllers;
using Xunit;

namespace MedicamentApp.Tests
{
    public class MenuPharmacistsTests
    {
        [Fact]
        public void Index_GET_ReturnsViewResult()
        {
            // Arrange
            var controller = new MenuPharmacists();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_POST_RedirectsToSelectedPage_WhenPageIsNotNullOrEmpty()
        {
            // Arrange
            var controller = new MenuPharmacists();
            var page = "/Home/Index";

            // Act
            var result = controller.Index(page) as RedirectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page, result.Url);
        }

        [Fact]
        public void Index_POST_RedirectsToIndex_WhenPageIsNullOrEmpty()
        {
            // Arrange
            var controller = new MenuPharmacists();

            // Act
            var result = controller.Index(string.Empty) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
