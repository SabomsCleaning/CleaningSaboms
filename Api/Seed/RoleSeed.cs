using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity;

namespace CleaningSaboms.Seed
{
    public static class RoleSeed
    {
        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = { "Admin", "User", "Developer", "ScheduleLayer" };
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { RoleName = role});
                }
            }
        }
    }
}
