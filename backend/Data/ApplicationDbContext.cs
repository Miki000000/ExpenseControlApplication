using ExpenseControlApplication.Data.Configurations;
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
        modelBuilder.ApplyConfiguration(new UserTableConfiguration());
        modelBuilder.ApplyConfiguration(new RolesTableConfiguration());
    }
}
