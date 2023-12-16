using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    public void SeedUsers()
    {
        if (userManager.FindByEmailAsync("user@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "user@localhost",
                Email = "user@localhost",
                NormalizedUserName = "USER@LOCALHOST",
                NormalizedEmail = "USER@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = userManager.CreateAsync(user, "Senha123!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser admin = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = userManager.CreateAsync(admin, "Senha123!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            roleManager.CreateAsync(role).Wait();
        }
    }
}
