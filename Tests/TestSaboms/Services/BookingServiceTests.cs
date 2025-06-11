using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Services;
using Moq;

namespace TestSaboms.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepoMock;
        private readonly Mock<IBookingFactory> _bookingFactoryRepoMock;
        private readonly Mock<ICustomerService> _customerServiceRepoMock;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingRepoMock = new Mock<IBookingRepository>();
            _bookingFactoryRepoMock = new Mock<IBookingFactory>();
            _bookingService = new BookingService(_bookingRepoMock.Object, _customerServiceRepoMock.Object, _bookingFactoryRepoMock.Object);
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldReturnSuccess_WhenValidBooking()
        {
            // Arrange
            _customerServiceRepoMock
                .Setup(s => s.CustomerExistId(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _bookingFactoryRepoMock
                .Setup(f => f.CreateFromDtoAsync(It.IsAny<BookingDto>()))
                .ReturnsAsync(new BookingEntity { Id = Guid.NewGuid() });

            _bookingRepoMock
                .Setup(r=>r.CreateBookingAsync(It.IsAny<BookingEntity>()))
                .Returns(Task.CompletedTask);

            var dto = new BookingDto
            {
                CustomerId = Guid.NewGuid(),
                ServiceTypeId = 1,
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
