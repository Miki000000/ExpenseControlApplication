using ExpenseControlApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseControlApplication.Data.Interfaces;

public interface IUserRepository
{
    public Task<IdentityResult> RegisterUserAsync(User user, string password);
    public Task<IdentityResult> RegisterRoleOnUserAsync(User user);
    public Task<User?> GetUserByUsername(string username);
    public Task<SignInResult> LoginAsync(User user, string password);
    public Task UpdateUser(User user);
    public Task<IQueryable<User>> GetAllUsers();
    public Task<bool> AuthorizeAdminOnUser(User user);
}