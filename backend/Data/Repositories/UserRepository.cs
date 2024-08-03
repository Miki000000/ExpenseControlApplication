using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data.Repositories;

public class UserRepository(UserManager<User> userManager, SignInManager<User> signInManager) 
    : IUserRepository
{
    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }
    public async Task<IdentityResult> RegisterRoleOnUserAsync(User user)
    {
        return await userManager.AddToRoleAsync(user, "User");
    }
    public async Task<User?> GetUserByUsername(string username)
    {
        return await userManager.Users
            .Include(u => u.Spendings)
            .FirstOrDefaultAsync(u => u.UserName!.ToLower() == username.ToLower());
    }
    public async Task<SignInResult> LoginAsync(User user, string password)
    {
        return await signInManager.CheckPasswordSignInAsync(user, password, false);
    }
    public async Task UpdateUser(User user)
    {
        await userManager.UpdateAsync(user);
    }
}