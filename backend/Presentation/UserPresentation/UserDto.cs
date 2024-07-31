using System.ComponentModel.DataAnnotations;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Presentation.UserPresentation;

public class UserDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Money { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal TotalGot { get; set; }
    public string Token { get; set; } = null!;
    public List<CreateSpendingDto> Spendings { get; set; }
}