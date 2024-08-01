using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Data.Interfaces;

public interface ISpendingRepository
{
    public Task<Spending?> CreateAsync(SpendingDto spendingDto, string username);
    public Task<List<Spending>?> GetUserSpendingAsync(string username);
    public Task<List<Spending>> GetAllUsersSpendingAsync();
    public Task<Spending?> DeleteUserSpendingAsync(string username, int spendingId);
    public Task<List<Spending>> DeleteAllUserSpendingAsync(string username);
    public Task<Spending?> UpdateSpendingAsync(UpdateSpendingDto spendingDto, string username);
}