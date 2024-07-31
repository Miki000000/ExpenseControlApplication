using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;

namespace ExpenseControlApplication.Data.Interfaces;

public interface IUserRepository
{
    // public Task<IQueryable<User>> GetAllAsync();
    // public Task<User?> GetByIdAsync(int id);
    // public Task<User> CreateAsync(User user);
    // public Task<User?> UpdateAsync(int id, UserDto user);
    // public Task<User?> DeleteAsync(int id);
    public Task<UserDto?> RegisterUserAsync(RegisterUserDto userDto);
    
    public Task<LoginResult> LoginAsync(LoginUserDto userDto);
}