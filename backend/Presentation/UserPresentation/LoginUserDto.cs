using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Presentation.UserPresentation;

public class LoginUserDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}