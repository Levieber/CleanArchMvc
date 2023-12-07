using Microsoft.AspNetCore.Identity;
using CleanArchMvc.Domain.Account;

namespace CleanArchMvc.Infra.Data.Identity;

public class AuthenticateService : IAuthenticate
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;

    public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUserAsync(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
        }

        return result.Succeeded;
    }
}
