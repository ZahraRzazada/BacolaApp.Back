using System;
using Bacola.Core.Entities;
using Bacola.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bacola.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),

                    });
                }
            }
            var userDb = await userManager.FindByNameAsync("SuperAdmin");
            if (userDb == null)
            {
                var user = new AppUser
                {
                    Email = "superadmin@gmail.com",
                    UserName = "Superadmin",
                    EmailConfirmed = true,
                    FullName = "SuperAdmin",
                };
                var result = await userManager.CreateAsync(user, "Superadmin123@");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin.ToString());
            }

        }
    }
}

