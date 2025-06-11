using CleaningSaboms.Dto;
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
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepoMock;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingRepoMock = new Mock<IBookingRepository>();
            _bookingService = new BookingService(_bookingRepoMock.Object);
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldReturnSuccess_WhenValidBooking()
        {
            // Arrange
            var dto = new BookingDto
            {
                CustomerId = Guid.NewGuid(),
                ServiceType = 1,
                Service = new ServiceType { Id = 1, Name = "Fönsterputs", BasePrice= 100 },
                ScheduleStartTime = DateTime.Now.AddDays(1),
                ScheduleEndTime = DateTime.Now.AddDays(1).AddHours(2),
            };

            // Act
            var result = await _bookingService.CreateBookingAsync(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
