using CleaningSaboms.Context;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Repositories
{
    public class BookingRepository(DataContext context) : IBookingRepository
    {
        private readonly DataContext _context = context;

        public async Task CreateBookingAsync(BookingEntity booking)
        {
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();
        }

        
    }
}
