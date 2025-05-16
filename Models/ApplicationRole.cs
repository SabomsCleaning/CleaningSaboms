using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CleaningSaboms.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Required]
        [MaxLength(30)]
        public string RoleName { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
