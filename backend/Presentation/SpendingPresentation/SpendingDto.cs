using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

public class SpendingDto
{
    [Required]
    public decimal ValueSpended { get; set; }
    public string? Description { get; set; }
    [Required]
    public string ItemBought { get; set; }
}
public class GetSpendingDto
{
    [Required]
    public decimal ValueSpended { get; set; }
    public DateTime SpendingDate { get; set; } = DateTime.Now;
    public string Username { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public string ItemBought { get; set; }

}

public class DeleteSpendingDto
{
    public int Id { get; set; }
    
    public decimal ValueSpended { get; set; }
}
public class CreateSpendingDto
{
    public int Id { get; set; }
    public decimal ValueSpended { get; set; }
    public string? Description { get; set; }
    public string ItemBought { get; set; }
    public DateTime SpendingDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
}

public class UpdateSpendingDto
{
    public string? Description { get; set; }
    public string ItemBought { get; set; }
    public decimal? ValueSpended { get; set; }
}