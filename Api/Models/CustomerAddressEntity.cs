namespace CleaningSaboms.Models
{
    public class CustomerAddressEntity
    {
        public Guid Id { get; set; }
        public string CustomerAddressLine { get; set; } = null!;
        public string CustomerCity { get; set; } = null!;
        public string CustomerPostalCode { get; set; } = null!;
        public Guid CustomerId { get; set; }
    }
}
