using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ExpenseControlApplication.Data.Entities;

public class User : IdentityUser
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal Money { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSpent { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalGot { get; set; }
    public virtual List<Spending> Spendings { get; set; } = new List<Spending>();
}