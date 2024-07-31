using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public class SpendingDto
{
    [Required]
    public decimal ValueSpended { get; set; }
}