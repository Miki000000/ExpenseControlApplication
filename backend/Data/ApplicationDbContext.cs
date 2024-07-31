using ExpenseControlApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    //CREATE TABLE users
    public DbSet<Spending> Spendings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Spendings)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole{ Name = "User", NormalizedName = "USER"}
        };
        modelBuilder.Entity<IdentityRole>()
            .HasData(roles);
    }
}
