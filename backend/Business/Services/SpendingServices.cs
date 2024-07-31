using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Business.Services;

public class SpendingServices(ApplicationDbContext context, UserManager<User> userManager) 
    : ISpendingServices
{
    public async Task<CreateSpendingDto?> CreateAsync(SpendingDto spendingDto, string username)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return null;
        var userId = await userManager.GetUserIdAsync(user);
        Spending spending = spendingDto.FromCreateToSpending(userId);
        await context.Spendings.AddAsync(spending);
        await context.SaveChangesAsync();
        user.Money -= spendingDto.ValueSpended;
        user.TotalSpent += spendingDto.ValueSpended;
        await userManager.UpdateAsync(user);
        return spending.FromSpendingToDto();
    }
}