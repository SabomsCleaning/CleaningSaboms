namespace CleaningSaboms.Models
{
    public class BookingCleanerEntity
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public BookingEntity Booking { get; set; } = null!;

        public string CleanerId { get; set; } = null!;
        public ApplicationUser Cleaner { get; set; } = null!;
    }


}
