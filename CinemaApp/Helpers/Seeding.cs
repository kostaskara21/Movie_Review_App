using System.Net;
using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Helpers
{
    public static class Seeding
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            


            // Seed Admin User
            if (await userManager.FindByEmailAsync("admin@site.com") == null)
            {
                var adminUser = new AppUser
                {
                    UserName = "admin@site.com",
                    Email = "admin@site.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Regular User
            if (await userManager.FindByEmailAsync("user@site.com") == null)
            {
                var regularUser = new AppUser
                {
                    UserName = "user@site.com",
                    Email = "user@site.com",
                    FirstName = "User",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(regularUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }
    }
}


