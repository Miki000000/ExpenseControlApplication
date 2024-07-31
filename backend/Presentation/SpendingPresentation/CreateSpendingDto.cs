namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public class CreateSpendingDto
{
    public int Id { get; set; }
    public decimal ValueSpended { get; set; }
    public DateTime SpendingDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
}