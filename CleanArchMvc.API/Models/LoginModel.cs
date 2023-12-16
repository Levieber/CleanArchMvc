using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long"), MinLength(6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
