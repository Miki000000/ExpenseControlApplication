using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Business.Interfaces;

public interface ISpendingServices
{
    public Task<CreateSpendingDto?> CreateAsync(SpendingDto spendingDto, string username);
    public Task<CreateSpendingDto?> UpdateAsync(UpdateSpendingDto spendingDto, int id, string username);
    public Task<List<DeleteSpendingDto>> DeleteAllAsync(string username);
    public Task<GetSpendingDto> GetUserSpendingAsync(string username, int spendingId);
    public Task<CreateSpendingDto> DeleteAsync(int spendingId, string username);
    public Task<List<GetSpendingDto>> GetAllUserSpendingAsync(string username);
}