using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Factories;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.UserPresentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data.Repositories;

public class UserRepository(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager) 
    : IUserRepository
{
    public async Task<Status> RegisterUserAsync(User user, string password)
    {
        var createdUser = await userManager.CreateAsync(user, password);
        if (!createdUser.Succeeded)
            return StatusFactory.CreateStatus(false,
                string.Join(", ", createdUser.Errors.ToList().Select(e => e.Description)));
        
        var roleResult = await userManager.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded)
        {
            return StatusFactory.CreateStatus(false,
                string.Join(", ", roleResult.Errors.ToList().Select(e => e.Description)));
        }
        return StatusFactory.CreateStatus(true);
    }

    public async Task<LoginResult> LoginAsync(LoginUserDto userDto)
    {
        var user = await userManager.Users.Include(u => u.Spendings)
            .FirstOrDefaultAsync(user => user.UserName!.ToLower() == userDto.Username.ToLower());
        if (user == null) return new LoginResult { Error = "Username" };
        var sign = await signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
        if (!sign.Succeeded) return new LoginResult { Error = "Password" };
        var token = tokenService.CreateToken(user);
        return new LoginResult
        {
            User = user.FromUserToNewUser(token)
        };
    }
}