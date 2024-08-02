using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Factories;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;

namespace ExpenseControlApplication.Data.Interfaces;

public interface IUserRepository
{
    public Task<Status> RegisterUserAsync(User user, string password);
    
    public Task<LoginResult> LoginAsync(LoginUserDto userDto);
}