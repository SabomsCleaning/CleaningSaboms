using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CleaningSaboms.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string UserFirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string UserLastName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string UserPhone { get; set; } = null!;


    }
}
