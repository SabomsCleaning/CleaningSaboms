using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Services;

namespace CleaningSaboms.Factory
{
    public class BookingFactory : IBookingFactory
    {
        private readonly ServiceTypeService _serviceTypeService;

        public BookingFactory(ServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        public async Task<BookingEntity> CreateFromDtoAsync(BookingDto dto)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeAsync(dto.ServiceTypeId);

            if (serviceType == null)
            {
                throw new InvalidOperationException("Ogiltig tjänstetyp");
            }

            return new BookingEntity
            {
                CustomerId = dto.CustomerId,
                ServiceTypeId = dto.ServiceTypeId,
                Service = serviceType,
                ScheduleStartTime = dto.ScheduleStartTime,
                ScheduleEndTime = dto.ScheduleEndTime
            };
        }
    }
}
