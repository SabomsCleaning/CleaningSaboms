using CleaningSaboms.Dto;
using CleaningSaboms.Factory;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using CleaningSaboms.Results;

namespace CleaningSaboms.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerService _customerService;
        private readonly IBookingFactory _bookingFactory;

        public BookingService(IBookingRepository bookingRepository, ICustomerService customerService, IBookingFactory bookingFactory)
        {
            _bookingRepository = bookingRepository;
            _customerService = customerService;
            _bookingFactory = bookingFactory;
        }

        public async Task<ServiceResult> CreateBookingAsync(BookingDto dto)
        {
            if (dto.ScheduleStartTime < DateTime.Now)
            {
                return ServiceResult.Fail("Starttiden har redan passerats", ErrorType.Forbidden);
            }

            if (dto.ScheduleEndTime < dto.ScheduleStartTime)
            {
                return ServiceResult.Fail("Sluttiden måste vara efter starttiden", ErrorType.Forbidden);
            }

            var customerExist = await _customerService.CustomerExistId(dto.CustomerId);

            if (!customerExist)
            {
                return ServiceResult.Fail("Kunde inte hitta kunden", ErrorType.NotFound);
            }

            var booking = await _bookingFactory.CreateFromDtoAsync(dto);

            await _bookingRepository.CreateBookingAsync(booking);
                return ServiceResult.Ok("Bokningen är skapad");
        }

        
    }
}
