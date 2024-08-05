using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.SpendingPresentation;
using ExpenseControlApplication.Utils.Exceptions;

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

    public async Task<List<GetSpendingDto>> GetAllUserSpendingAsync(string username)
    {
        var user = await userRepo.GetUserByUsername(username) ??
                   throw new NotFoundException("User not found");
        return user.Spendings.Select(s => s.FromSpendingToGetSpendingDto(username)).ToList();
    }
    public async Task<CreateSpendingDto?> UpdateAsync(UpdateSpendingDto spendingDto, string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user is null)
            throw new NotFoundException("User not found!");
        if (!user.Spendings.Any())
            throw new NotFoundException("User does not have any spendings");
        var spending = user.Spendings.FirstOrDefault(s => s.Id == spendingDto.Id);
        if (spending is null)
            throw new NotFoundException("Spending not found!");
        spending.ValueSpended = spendingDto.ValueSpended;
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