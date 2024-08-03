using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Data.Interfaces;

public interface ISpendingRepository
{
    public Task<Spending> CreateAsync(Spending spendingDto);
    public Task<Spending?> DeleteUserSpendingAsync(Spending spending);
    public Task<List<Spending>> DeleteAllUserSpendingAsync(List<Spending> spendings);
    public Task UpdateSpendingAsync();
    public Task<bool> SpendingExistsAsync(Spending spending);
}