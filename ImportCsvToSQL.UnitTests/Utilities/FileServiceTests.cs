using Moq;
using NUnit.Framework;
using ImportCsvToSQL.Models;
using ImportCsvToSQL.Utilities;
using System;
using System.Collections.Generic;
using System.Web;

namespace ImportCsvToSQL.UnitTests.Utilities
{
    [TestFixture]
    class FileServiceTests
    {
        private Mock<HttpPostedFileBase> _file;
        private Mock<IPathReader> _pathReader;
        private Mock<IFileRepository> _fileRepository;
        private FileService _service;

        [SetUp]
        public void SetUp()
        {
            _file = new Mock<HttpPostedFileBase>();
            _pathReader = new Mock<IPathReader>();
            _fileRepository = new Mock<IFileRepository>();
            _service = new FileService(_pathReader.Object, _fileRepository.Object);
        }

        [Test]
        public void FilePath_EmptyFileName_ReturnError()
        {
            //Arrange
            _file.Setup(f => f.FileName).Returns("dataset.csv");
            _pathReader.Setup(pr => pr.GetFileNameWithoutExtension("dataset.csv")).Returns("");

            //Act
            var result = _service.FilePath(_file.Object);

            //Assert
            Assert.That(result, Does.Contain("file name").IgnoreCase);
        }


        [Test]
        public void FilePath_EmptyPath_ReturnError()
        {
            _file.Setup(f => f.FileName).Returns("dataset.csv");
            _pathReader.Setup(pr => pr.GetFileNameWithoutExtension("dataset.csv")).Returns("dataset");
            _pathReader.Setup(pr => pr.MapPath("~/Csvs")).Returns("");
            _pathReader.Setup(pr => pr.Combine(@"C:\Csvs", "dataset.csv")).Returns("");

            var result = _service.FilePath(_file.Object);

            Assert.That(result, Does.Contain("path").IgnoreCase);
        }

        [Test]
        public void GetEmployeesFromCsv_NoEmployees_ReturnNull()
        {
            _fileRepository.Setup(r => r.GetEmployees(@"C:\Csvs")).Returns(new List<Employee>());

            var result = _service.GetEmployeesFromCsv(@"C:\Csvs\dataset.csv");

            Assert.That(result, Is.EqualTo(""));
        }
        
        [Test]
        public void GetEmployeesFromCsv_OneOrMoreEmployees_ReturnList()
        {
            _fileRepository.Setup(r => r.GetEmployees(It.IsAny<string>())).Returns(new List<Employee>
            {
                new Employee {
                    //Id = 1,
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
                },
                new Employee {
                    //Id = 2,
                    Payroll_Number = "EFGH456",
                    Forenames = "Mary",
                    Surname = "Smith",
                    Date_of_Birth = DateTime.Parse("2/2/99"),
                    Telephone = "1112223344",
                    Mobile = "5556667788",
                    Address = "789 street",
                    Address_2 = "Apt 098",
                    Postcode = "98765",
                    EMail_Home = "inbox@email.xyz",
                    Start_Date = DateTime.Parse("2/2/99")

                }
            });

            var result = _service.GetEmployeesFromCsv(@"C:\Csvs\dataset.csv");

            Assert.That(result.Count, Is.EqualTo(2));
        }

    }
}
