using System.ComponentModel.DataAnnotations;

namespace CleaningSaboms.Models
{
    public class CustomerAddressEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerAddressLine { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CustomerCity { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CustomerPostalCode { get; set; } = null!;

        [Required]
        public Guid CustomerId { get; set; }
    }
}
