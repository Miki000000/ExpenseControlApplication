using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.SpendingPresentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Business.Services;

public class SpendingServices(ISpendingRepository spendingRepo) 
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
        var spending = await spendingRepo.CreateAsync(spendingDto, username);
        return spending?.FromSpendingToDto();
    }

    public async Task<CreateSpendingDto?> UpdateAsync(UpdateSpendingDto spendingDto, string username)
    {
        var updatedSpending = await spendingRepo.UpdateSpendingAsync(spendingDto, username); 
        return updatedSpending?.FromSpendingToDto();
    }

    public async Task<CreateSpendingDto?> DeleteAsync(int spendingId, string username)
    {
    }
}