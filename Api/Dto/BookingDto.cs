using CleaningSaboms.Models;

namespace CleaningSaboms.Dto
{
    public class BookingDto
    {
        public Guid CustomerId { get; set; }
        public int ServiceTypeId { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
    }
}
