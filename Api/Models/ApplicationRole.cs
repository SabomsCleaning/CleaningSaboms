using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CleaningSaboms.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
