namespace CleaningSaboms.Models
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public Guid CustomerAddressId { get; set; }
        public CustomerAddressEntity CustomerAddress { get; set; } = null!;

    }
}
