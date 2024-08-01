using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public class SpendingDto
{
    [Required]
    public decimal ValueSpended { get; set; }
}
public class CreateSpendingDto
{
    public int Id { get; set; }
    public decimal ValueSpended { get; set; }
    public DateTime SpendingDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
}

public class UpdateSpendingDto
{
    public int Id { get; set; }
    public decimal ValueSpended { get; set; }
}