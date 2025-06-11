using CleaningSaboms.Dto;
using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IBookingFactory
    {
        Task<BookingEntity> CreateFromDtoAsync(BookingDto dto);
    }
}
