using CleaningSaboms.Context;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;

namespace CleaningSaboms.Repositories
{
    public class BookingRepository(DataContext context) : IBookingRepository
    {
        private readonly DataContext _context = context;

        public async Task CreateBooking(BookingEntity booking)
        {
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();
        }
    }
}
