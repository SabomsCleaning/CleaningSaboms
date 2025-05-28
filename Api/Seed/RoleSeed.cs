using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;

namespace CleaningSaboms.Seed
{
    public static class RoleSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleName = { "Admin", "User", "Developer", "ScheduleLayer", "Customer" };
            foreach (var role in roleName)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                    };

                    await roleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
