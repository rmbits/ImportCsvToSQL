using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ImportCsvToSQL.Controllers;
using ImportCsvToSQL.Models;
using ImportCsvToSQL.Utilities;

namespace ImportCsvToSQL.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IDBRepository> _dbRepository;

        [SetUp]
        public void SetUp()
        {
            _dbRepository = new Mock<IDBRepository>();
            _controller = new HomeController(_dbRepository.Object);
        }

        [Test]
        public void Index_WhenCalled_ReturnActionResult()
        {
            // Arrange

            // Act
            var result = _controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

        [Test]
        public void GetEmployees_WhenCalled_ReturnJsonResult()
        {
            var result = _controller.GetEmployees();

            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        [TestCase(null)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Update_IdIsZeroNullNegative_ReturnViewResult(int id)
        {
            var result = _controller.Update(id);

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Update_IdExists_ReturnEmployee(int id)
        {
            var _dbRepository = new Mock<IDBRepository>();
            _dbRepository.Setup(r => r.GetEmployee(It.IsAny<int>())).Returns(new Employee
            {
                Id = 1,
                Payroll_Number = "ABCD123",
                Forenames = "John",
                Surname = "Doe",
                Date_of_Birth = DateTime.Parse("1/1/00"),
                Telephone = "5556667788",
                Mobile = "9998887766",
                Address = "123 street",
                Address_2 = "Apt 456",
                Postcode = "123456",
                EMail_Home = "email@inbox.xyz",
                Start_Date = DateTime.Parse("1/1/00")
            });

            var result = _controller.Update(id) as ViewResult;

            Assert.That(result, Is.Not.Null);
        }
    }
}
