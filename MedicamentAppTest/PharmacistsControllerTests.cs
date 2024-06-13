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
    public class PharmacistsControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithAListOfPharmacists()
        {
            // Arrange
            var mockContext = new Mock<MedicamentAppContext>();
            var mockPharmacistsSet = new Mock<DbSet<Pharmacists>>();

            var pharmacists = new List<Pharmacists>
            {
                new Pharmacists
                {
                    Идентификатор = 1,
                    ФИО = "John Doe",
                    Дата_приема = DateTime.Now,
                    Статус = "Active"
                },
                new Pharmacists
                {
                    Идентификатор = 2,
                    ФИО = "Jane Smith",
                    Дата_приема = DateTime.Now,
                    Статус = "Inactive"
                }
            }.AsQueryable();

            mockPharmacistsSet.As<IQueryable<Pharmacists>>().Setup(m => m.Provider).Returns(pharmacists.Provider);
            mockPharmacistsSet.As<IQueryable<Pharmacists>>().Setup(m => m.Expression).Returns(pharmacists.Expression);
            mockPharmacistsSet.As<IQueryable<Pharmacists>>().Setup(m => m.ElementType).Returns(pharmacists.ElementType);
            mockPharmacistsSet.As<IQueryable<Pharmacists>>().Setup(m => m.GetEnumerator()).Returns(pharmacists.GetEnumerator());

            mockContext.Setup(c => c.Pharmacists).Returns(mockPharmacistsSet.Object);

            var controller = new PharmacistsController(mockContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Pharmacists>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
