using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Presentation.UserPresentation;

public class RegisterUserDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public decimal Money { get; set; }
}