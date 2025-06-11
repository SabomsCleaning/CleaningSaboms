namespace CleaningSaboms.Models
{
    public class BookingEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int BookingNumber { get; set; }
        public Guid CustomerId { get; set; }

        public int ServiceType { get; set; }
        public ServiceType Service { get; set; } = null!;

        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }

        public ICollection<BookingCleanerEntity> Cleaners { get; set; }= new List<BookingCleanerEntity>();
    }
}
