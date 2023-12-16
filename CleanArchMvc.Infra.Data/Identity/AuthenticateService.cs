using Microsoft.AspNetCore.Identity;
using CleanArchMvc.Domain.Account;

namespace CleanArchMvc.Infra.Data.Identity;

public class AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthenticate
{
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUserAsync(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
        }

        return result.Succeeded;
    }
}
