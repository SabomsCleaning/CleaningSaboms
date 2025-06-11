using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBooking (BookingEntity booking);
    }
}
