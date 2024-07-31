using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Presentation.UserPresentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Data.Repositories;

public class UserRepository(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager) 
    : IUserRepository

{
    public async Task<UserDto?> RegisterUserAsync(RegisterUserDto userDto)
    {
        var user = new User
        {
            UserName = userDto.Username,
            Email = userDto.Email,
            Money = userDto.Money,
            TotalGot = userDto.Money,
            TotalSpent = 0
        };
        var createdUser = await userManager.CreateAsync(user, userDto.Password);
        if (!createdUser.Succeeded) return null;
        var roleResult = await userManager.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded) return null;
        var securityToken = tokenService.CreateToken(user);
        return user.FromUserToNewUser(securityToken);
    }

    public async Task<LoginResult> LoginAsync(LoginUserDto userDto)
    {
        var user = await userManager.Users.Include(u => u.Spendings)
            .FirstOrDefaultAsync(user => user.UserName!.ToLower() == user.UserName.ToLower());
        if (user == null) return new LoginResult { Error = "Invalid Username" };
        var sign = await signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
        if (!sign.Succeeded) return new LoginResult { Error = "Invalid Password" };
        var token = tokenService.CreateToken(user);
        return new LoginResult
        {
            User = user.FromUserToNewUser(token)
        };
    }
}