using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControlApplication.Data.Configurations;

public class RolesTableConfiguration: IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole{ Name = "User", NormalizedName = "USER"}
        };
        builder
            .HasData(roles);
    }
}