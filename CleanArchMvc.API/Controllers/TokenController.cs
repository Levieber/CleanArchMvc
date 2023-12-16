using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(IAuthenticate authenticateService, IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterModel model)
    {
        var result = await authenticateService.RegisterUserAsync(model.Email, model.Password);

        if (result)
        {
            return Ok($"User {model.Email} was created successfully");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return BadRequest(ModelState);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserToken>> Login(LoginModel model)
    {
        var result = await authenticateService.AuthenticateAsync(model.Email, model.Password);

        if(result)
        {
            return GenerateToken(model);
        } else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel model)
    {
        Claim[] claims =
        [
            new Claim("email", model.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new ArgumentException("Jwt Secret Key must be set")));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(10);

        JwtSecurityToken token = new(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"], claims: claims, signingCredentials: credentials, expires: expiration);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
