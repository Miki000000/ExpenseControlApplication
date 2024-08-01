using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.SpendingPresentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data.Repositories;

public class SpendingRepository(ApplicationDbContext context, UserManager<User> userManager)
    : ISpendingRepository
{
    public async Task<Spending?> CreateAsync(SpendingDto spendingDto, string username)
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
        return spending;
    }

    public async Task<List<Spending>?> GetUserSpendingAsync(string username)
    {
        var user = await userManager.Users
            .Include(u => u.Spendings)
            .FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return null;
        
        var spendings = user.Spendings.ToList();
        return spendings;
    }

    public async Task<List<Spending>> GetAllUsersSpendingAsync()
    {
        var spendings = await context.Spendings.ToListAsync();
        return spendings;
    }

    public async Task<Spending?> DeleteUserSpendingAsync(string username, int spendingId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return null;
        var userSpending = await context.Spendings.FirstOrDefaultAsync(s => s.UserId == user.Id && s.Id == spendingId);
        if (userSpending == null) return null;
        context.Spendings.Remove(userSpending);
        await context.SaveChangesAsync();
        return userSpending;
    }

    public async Task<List<Spending>> DeleteAllUserSpendingAsync(string username)
    {
        var user = await userManager.Users
            .Include(u => u.Spendings)
            .FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return new List<Spending>();
        var userSpendings = await context.Spendings.Where(s => s.UserId == user.Id).ToListAsync();
        return userSpendings;
    }

    public async Task<Spending?> UpdateSpendingAsync(UpdateSpendingDto spendingDto, string username)
    {
        var user = await userManager.Users
            .Include(u => u.Spendings)
            .FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return null;
        var userSpending = user.Spendings.FirstOrDefault(s => s.Id == spendingDto.Id);
        if (userSpending == null) return null;
        var spending = await context.Spendings.FirstOrDefaultAsync(u => u.Id == userSpending.Id);
        if (spending == null)
        {
            user.Spendings.Remove(userSpending);
            await userManager.UpdateAsync(user);
            return null;
        }
        spending.SpendingDate = DateTime.Now;
        spending.ValueSpended = spendingDto.ValueSpended;
        await context.SaveChangesAsync();
        return spending;
    }
}