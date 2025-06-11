using CleaningSaboms.Models;

namespace CleaningSaboms.Dto
{
    public class BookingDto
    {
        public Guid CustomerId { get; set; }

        public int ServiceType { get; set; }
        public ServiceType Service { get; set; } = null!;

        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
    }
}
