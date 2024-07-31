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
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
}