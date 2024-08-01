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
        var deletedSpending = await spendingRepo.DeleteUserSpendingAsync(username, spendingId);
        return deletedSpending?.FromSpendingToDto();
    }
}