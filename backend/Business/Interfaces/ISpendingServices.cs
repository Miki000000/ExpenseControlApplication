using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Business.Interfaces;

public interface ISpendingServices
{
    public Task<CreateSpendingDto?> CreateAsync(SpendingDto spendingDto, string username);
}