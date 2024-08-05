using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseControlApplication.Data.Entities;

public class Spending
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValueSpended { get; set; }
    public DateTime SpendingDate { get; set; } = DateTime.Now;
    [Length(1, 300, ErrorMessage = "The description should be at least 5 characters long or maximum 300 characters")]
    public string? Description { get; set; }
    [Length(1, 100, ErrorMessage = "The item name should be at maximum 100 characters long")]
    public string ItemBought { get; set; }
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
}