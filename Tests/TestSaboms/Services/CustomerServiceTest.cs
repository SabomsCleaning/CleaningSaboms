using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;
using CleaningSaboms.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleaningSaboms.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _service = new CustomerService(_customerRepoMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_Should_Fail_If_Customer_Exists()
        {
            // Arrange
            var dto = new CustomerDto { CustomerFirstName = "Anna", CustomerLastName = "Karlsson", CustomerEmail = "anna@test.com" };
            _customerRepoMock.Setup(r => r.CustomerExistsAsync(dto)).ReturnsAsync(true);

            // Act
            var result = await _service.CreateCustomer(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Kunden finns redan i databasen", result.Message);
        }

        [Fact]
        public async Task CreateCustomer_Should_Fail_If_Address_Exists()
        {
            // Arrange
            var dto = new CustomerDto { CustomerFirstName = "Bo", CustomerLastName = "Svensson", CustomerEmail = "bo@test.com" };
            _customerRepoMock.Setup(r => r.CustomerExistsAsync(dto)).ReturnsAsync(false);
            _customerRepoMock.Setup(r => r.AddressExistsAsync(dto)).ReturnsAsync(true);

            // Act
            var result = await _service.CreateCustomer(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Kundens adress finns redan i databasen", result.Message);
        }

        [Fact]
        public async Task CreateCustomer_Should_Succeed_If_Customer_And_Address_Not_Exists()
        {
            // Arrange
            var dto = new CustomerDto { CustomerFirstName = "Cecilia", CustomerLastName = "Nilsson", CustomerEmail = "cecilia@test.com" };
            _customerRepoMock.Setup(r => r.CustomerExistsAsync(dto)).ReturnsAsync(false);
            _customerRepoMock.Setup(r => r.AddressExistsAsync(dto)).ReturnsAsync(false);
            //_customerRepoMock.Setup(r => r.CreateCustomer(It.IsAny<CustomerEntity>())).ReturnsAsync(true);

            // Act
            var result = await _service.CreateCustomer(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Skapad", result.Message);
        }

        [Fact]
        public async Task GetCustomer_Should_Fail_If_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _customerRepoMock.Setup(r => r.GetCustomerById(id)).ReturnsAsync((CustomerEntity?)null);

            // Act
            var result = await _service.GetCustomerById(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Kund hittades inte.", result.Message);
        }

        [Fact]
        public async Task GetCustomer_Should_Succeed_If_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new CustomerEntity
            {
                Id = id,
                CustomerEmail = "test@test.com",
                CustomerFirstName = "Anna",
                CustomerLastName = "Svensson",
                CustomerAddress = new CustomerAddressEntity
                {
                    CustomerAddressLine = "Gatan 1",
                    CustomerCity = "Stockholm",
                    CustomerPostalCode = "12345"
                }
            };

            _customerRepoMock.Setup(r => r.GetCustomerById(id)).ReturnsAsync(entity);

            // Act
            var result = await _service.GetCustomerById(id);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Kund hittades.", result.Message);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_CustomerDtos_When_Customers_Exist()
        {
            // Arrange
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerFirstName = "Alice",
                    CustomerLastName = "Andersson",
                    CustomerEmail = "alice@example.com",
                    CustomerAddress = new CustomerAddressEntity
                    {
                        CustomerAddressLine = "Storgatan 1",
                        CustomerCity = "Stockholm",
                        CustomerPostalCode = "12345"
                    }
                }
            };

            _customerRepoMock.Setup(r => r.GetAllCustomers()).ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Alice", result.First().CustomerFirstName);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_EmptyList_When_No_Customers()
        {
            // Arrange
            _customerRepoMock.Setup(r => r.GetAllCustomers()).ReturnsAsync(new List<CustomerEntity>());

            // Act
            var result = await _service.GetAllCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_EmptyList_When_Null()
        {
            // Arrange
            _customerRepoMock.Setup(r => r.GetAllCustomers()).ReturnsAsync((IEnumerable<CustomerEntity>?)null);

            // Act
            var result = await _service.GetAllCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
