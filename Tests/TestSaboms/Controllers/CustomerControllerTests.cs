using CleaningSaboms.Controllers;
using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleaningSaboms.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<ILogger<CustomerController>> _loggerMock;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _loggerMock = new Mock<ILogger<CustomerController>>();
            _controller = new CustomerController(_customerServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_Returns_Ok_When_Success()
        {
            // Arrange
            var dto = new CustomerDto
            {
                CustomerFirstName = "Anna",
                CustomerLastName = "Svensson",
                CustomerEmail = "anna@test.com",
                CustomerAddressLine = "Gatan 1",
                CustomerCity = "Stockholm",
                CustomerPostalCode = "12345"
            };

            var adress = new CustomerAddressEntity
            {
                CustomerAddressLine = dto.CustomerAddressLine,
                CustomerCity = dto.CustomerCity,
                CustomerPostalCode = dto.CustomerPostalCode
            };

            var entity = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                CustomerFirstName = dto.CustomerFirstName,
                CustomerLastName = dto.CustomerLastName,
                CustomerEmail = dto.CustomerEmail,
                CustomerAddress = adress
            };

            _customerServiceMock
                .Setup(s => s.CreateCustomer(It.IsAny<CustomerDto>()))
                .ReturnsAsync(ServiceResult<CustomerEntity>.Ok(entity, "Skapad"));

            // Act
            var result = await _controller.CreateCustomer(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // ✅ kontrollera att det är 200 OK
            var returnValue = Assert.IsType<CustomerDto>(okResult.Value); // ✅ kontrollera typ

            // Exempel på valfri detaljkontroll
            Assert.Equal(dto.CustomerFirstName, returnValue.CustomerFirstName);
        }


        [Fact]
        public async Task CreateCustomer_Returns_BadRequest_When_Service_Fails()
        {
            var dto = new CustomerDto();

            _customerServiceMock.Setup(s => s.CreateCustomer(dto))
                .ReturnsAsync(ServiceResult<CustomerEntity>.Fail("Dublett"));

            var result = await _controller.CreateCustomer(dto);

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dublett", badResult.Value);
        }

        [Fact]
        public async Task CreateCustomer_Returns_BadRequest_When_Null()
        {
            var result = await _controller.CreateCustomer(null);

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Customer cannot be null.", badResult.Value);
        }

        [Fact]
        public async Task GetCustomer_Returns_Ok_When_Found()
        {
            var id = Guid.NewGuid();
            var entity = new CustomerEntity { Id = id, CustomerEmail = "test@test.com" };

            _customerServiceMock.Setup(s => s.GetCustomer(id))
                .ReturnsAsync(ServiceResult<CustomerEntity>.Ok(entity, "Hittad"));

            var result = await _controller.GetCustomer(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ServiceResult<CustomerEntity>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Hittad", returnValue.Message);
        }

        [Fact]
        public async Task GetCustomer_Returns_NotFound_When_Null()
        {
            var id = Guid.NewGuid();

            _customerServiceMock.Setup(s => s.GetCustomer(id))
                .ReturnsAsync(ServiceResult<CustomerEntity>.Fail("Kund saknas"));

            var result = await _controller.GetCustomer(id);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Customer not found.", notFound.Value);
        }

        [Fact]
        public async Task GetAllCustomers_Returns_Ok_When_Customers_Found()
        {
            var list = new List<CustomerDto>
            {
                new CustomerDto
                {
                    CustomerFirstName = "Test",
                    CustomerLastName = "Person",
                    CustomerEmail = "test@example.com"
                }
            };

            _customerServiceMock.Setup(s => s.GetAllCustomers())
                .ReturnsAsync(list);

            var result = await _controller.GetAllCustomers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(okResult.Value);
            Assert.Single(returnList);
        }

        [Fact]
        public async Task GetAllCustomers_Returns_NotFound_When_Empty()
        {
            _customerServiceMock.Setup(s => s.GetAllCustomers())
                .ReturnsAsync(new List<CustomerDto>());

            var result = await _controller.GetAllCustomers();

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No customers found.", notFound.Value);
        }
    }
}

