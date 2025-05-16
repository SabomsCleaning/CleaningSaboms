namespace CleaningSaboms.Models
{
    public class CustomerEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
    }
}
