using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("user@localhost").Result == null)
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

            var result = _userManager.CreateAsync(user, "Senha123!").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
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

            var result = _userManager.CreateAsync(admin, "Senha123!").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(admin, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            _roleManager.CreateAsync(role).Wait();
        }
    }
}
