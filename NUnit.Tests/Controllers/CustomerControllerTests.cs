using AutoMapper;
using DotNetWebApi.Business.Customers.HLC;
using DotNetWebApi.Business.Customers.HLC.Models;
using DotNetWebApi.Controllers;
using DotNetWebApi.Customers.Delegates;
using DotNetWebApi.Customers.Delegates.ViewModels;
using DotNetWebApi.Data.Customers.LLC;
using DotNetWebApi.Data.Customers.LLC.DataModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace NUnit.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        CustomersController controller;
        IMapper mapper;

        [SetUp]
        public void Init()
        {
            mapper = AutomapperTestHelper.Mapper;
            var customerRepository = MockedRepository(new Mock<ICustomerRepository>());
            var customerHlc = new CustomerHlc(customerRepository, mapper);
            var customerDelegate = new CustomerDelegate(customerHlc, mapper);

            controller = new CustomersController(customerDelegate);
        }

        [Test]
        public void Test_GetCustomerById()
        {
            // Arrange

            // Act
            var result = controller.GetCustomerById(Guid.NewGuid());
            var contentResult = result as OkNegotiatedContentResult<CustomerViewModel>;
            var content = contentResult.Content;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(mockCustomer.Guid, content.Guid);
            Assert.AreEqual(mockCustomer.Number, content.Number);
            Assert.AreEqual(mockCustomer.Address.City, content.Address.City);
        }

        [Test]
        public void Test_AddCustomer()
        {
            // Arrange
            var customerModel = mapper.Map<CustomerModel>(mockCustomer);
            var customerVM = mapper.Map<CustomerViewModel>(customerModel);

            // Act
            var result = controller.AddCustomer(customerVM);
            var contentResult = result as OkNegotiatedContentResult<CustomerViewModel>;
            var content = contentResult.Content;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(mockCustomer.Guid, content.Guid);
            Assert.AreEqual(mockCustomer.Number, content.Number);
            Assert.AreEqual(mockCustomer.Address.City, content.Address.City);
        }


        private ICustomerRepository MockedRepository(Mock<ICustomerRepository> mock)
        {
            mock.Setup(r => r.GetAll()).Returns(
                Task.FromResult<IEnumerable<Customer>>(new List<Customer> { mockCustomer, mockCustomer })
            );
            mock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(mockCustomer);
            mock.Setup(r => r.Add(It.IsAny<Customer>())).Returns(mockCustomer);
            return mock.Object;
        }

        private Customer mockCustomer = new Customer
        {
            Guid = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Name",
            Number = "555-555-5555",
            Address = new Address
            {
                Street = "12345 Test St",
                City = "Somewhere",
                State = "NC",
                Zip = "12345"
            }
        };
    }
}
