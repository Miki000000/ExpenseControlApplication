using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.SpendingPresentation;
using ExpenseControlApplication.Utils.Exceptions;
using ExpenseControlApplication.Utils.Extensions;
using ExpenseControlApplication.Utils.Helpers;

namespace ExpenseControlApplication.Business.Services;

public class SpendingServices(ISpendingRepository spendingRepo, IUserRepository userRepo) 
    : ISpendingServices
{
    public async Task<CreateSpendingDto?> CreateAsync(SpendingDto spendingDto, string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user == null)
            throw new NotFoundException("User not found!");
        if (user.Money <= 0)
            throw new InvalidEntryException("User does not have the money to do that!");
        if (user.Money - spendingDto.ValueSpended < 0)
            throw new InvalidEntryException("User does not have the money to do that!");
        var spending = spendingDto.FromCreateToSpending(user.Id);
        var newSpending = await spendingRepo.CreateAsync(spending);
        user.Money -= spendingDto.ValueSpended;
        user.TotalSpent += spendingDto.ValueSpended;
        await userRepo.UpdateUser(user);
        return newSpending.FromSpendingToDto();
    }

    public async Task<GetSpendingDto> GetUserSpendingAsync(string username, int spendingId)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user == null)
            throw new NotFoundException("User not found!");
        var spending = user.Spendings
            .FirstOrDefault(s => s.Id == spendingId);
        if (spending == null)
            throw new NotFoundException("Spending not found!");
        if (!await spendingRepo.SpendingExistsAsync(spending))
            throw new NotFoundException("Spending does not exist in the database!");
        return spending.FromSpendingToGetSpendingDto(user.UserName!);
    }

    public async Task<List<GetSpendingDto>> GetAllUserSpendingAsync(string username, QueryObject queryObject)
    {
        var user = await userRepo.GetUserByUsername(username) ??
                   throw new NotFoundException("User not found");
        var spendings = user.Spendings.AsQueryable() ??
                        throw new NotFoundException("User has not spendings!");
        spendings = spendings.FilterBy(new() { filter = "ItemName" }, queryObject.ItemName);
        spendings = spendings.FilterBy(new() {filter = "Description"} ,queryObject.Description);
        spendings = spendings.SortSpendings(queryObject.SortBy, queryObject.IsDescending);
        int skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        return spendings
            .Skip(skipNumber)
            .Take(queryObject.PageSize)
            .Select(s => s.FromSpendingToGetSpendingDto(username))
            .ToList();
    }
    
    public async Task<CreateSpendingDto?> UpdateAsync(UpdateSpendingDto spendingDto, int id, string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user is null)
            throw new NotFoundException("User not found!");
        if (!user.Spendings.Any())
            throw new NotFoundException("User does not have any spendings");
        var spending = user.Spendings.FirstOrDefault(s => s.Id == id);
        if (spending is null)
            throw new NotFoundException("Spending not found!");
        if (spendingDto.ValueSpended > spending.ValueSpended)
        {
            user.TotalSpent += (decimal)spendingDto.ValueSpended;
            user.Money -= user.Money - (decimal)spendingDto.ValueSpended == 0 ?
                throw new InvalidEntryException("User does not have this money.") :
                (decimal)spendingDto.ValueSpended;
        }
        else if (spendingDto.ValueSpended < spending.ValueSpended)
        {
            user.TotalSpent -= (decimal)spendingDto.ValueSpended;
            user.Money += (decimal)spendingDto.ValueSpended;
        }
        spending.ValueSpended = spendingDto.ValueSpended ?? spending.ValueSpended;
        spending.Description = spendingDto.Description ?? spending.Description;
        await spendingRepo.UpdateSpendingAsync();
        return spending.FromSpendingToDto();
    }
    public async Task<CreateSpendingDto> DeleteAsync(int spendingId, string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user == null)
            throw new NotFoundException($"User: {username}");
        if (!user.Spendings.Any())
            throw new NotFoundException("Spendings");
        var spending = user.Spendings.FirstOrDefault(s => s.Id == spendingId);
        if (spending == null)
            throw new NotFoundException("Spending");
        user.Money += spending.ValueSpended;
        user.TotalSpent -= spending.ValueSpended;
        await userRepo.UpdateUser(user);
        await spendingRepo.DeleteUserSpendingAsync(spending);
        return spending.FromSpendingToDto();
    }

    public async Task<List<DeleteSpendingDto>> DeleteAllAsync(string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user == null)
            throw new NotFoundException($"User: {username}");
        if (!user.Spendings.Any())
            throw new NotFoundException("Spendings");
        var spendingsToDelete = user.Spendings;
        await spendingRepo.DeleteAllUserSpendingAsync(spendingsToDelete);
        return user.Spendings.Select(s => s.FromSpendingToDeleteDto()).ToList();
    }
}