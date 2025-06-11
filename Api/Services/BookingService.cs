using CleaningSaboms.Dto;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Results;

namespace CleaningSaboms.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerService _customerService;

        public BookingService(IBookingRepository bookingRepository, ICustomerService customerService)
        {
            _bookingRepository = bookingRepository;
            _customerService = customerService;
        }

        public async Task<ServiceResult> CreateBookingAsync(BookingDto dto)
        {
            if (dto.ScheduleStartTime < DateTime.Now)
            {
                return ServiceResult.Fail("Starttiden har redan passerats", ErrorType.Forbidden);
            }

            if (dto.ScheduleEndTime < dto.ScheduleStartTime)
            {
                return ServiceResult.Fail("Startiden är efter avsluttiden", ErrorType.Forbidden);
            }

            var customerExist = await _customerService.CustomerExistId(dto.CustomerId);

            if (!customerExist)
            {
                return ServiceResult.Fail("Kunde inte hitta kunden", ErrorType.NotFound);
            }


                return ServiceResult.Ok("Bokningen är skapad");
        }
    }
}
