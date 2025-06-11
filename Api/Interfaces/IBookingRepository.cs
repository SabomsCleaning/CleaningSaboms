using CleaningSaboms.Models;

namespace CleaningSaboms.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync (BookingEntity booking);
        
    }
}
