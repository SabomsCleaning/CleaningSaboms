using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSaboms.Services
{
    public class ServiceTypeServiceTests
    {
        private readonly Mock<IServiceTypeRepository> _serviceRepoMock;
        private readonly ServiceTypeService _service;

        public ServiceTypeServiceTests()
        {
            _serviceRepoMock = new Mock<IServiceTypeRepository>();
            _service = new ServiceTypeService(_serviceRepoMock.Object);
        }

        [Fact]
        public async Task GetServiceTypeAsync()
        {
            // Act
            var expectId = 1;
            var expectedServiceType = new ServiceType
            {
                Id = 1,
                Name = "Fönsterputs",
                BasePrice = 500
            };
            // Arrange
            // Assert
        }
    }
}
