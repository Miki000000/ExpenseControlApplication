using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data.Repositories;

public class SpendingRepository(ApplicationDbContext context)
    : ISpendingRepository
{
    public async Task<Spending> CreateAsync(Spending spending)
    {
        await context.Spendings.AddAsync(spending);
        await context.SaveChangesAsync();
        return spending;
    }

    public async Task<Spending?> DeleteUserSpendingAsync(Spending spending)
    {
        context.Spendings.Remove(spending);
        await context.SaveChangesAsync();
        return spending;
    }

    public async Task<List<Spending>> DeleteAllUserSpendingAsync(List<Spending> spendings)
    {
        context.RemoveRange(spendings);
        await context.SaveChangesAsync();
        return spendings;
    }

    public async Task<bool> SpendingExistsAsync(Spending spending)
    {
        return await context.Spendings.ContainsAsync(spending);
    }
    public async Task UpdateSpendingAsync()
    {
        await context.SaveChangesAsync();
    }
}