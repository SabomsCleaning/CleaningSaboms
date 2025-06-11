using CleaningSaboms.Dto;
using CleaningSaboms.Results;

namespace CleaningSaboms.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResult> CreateBookingAsync(BookingDto dto);
    }
}
