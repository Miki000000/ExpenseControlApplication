using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Business.Services;

public class UserServices(IUserRepository userRepo) 
    : IUserServices
{
    public async Task<UserDto?> RegisterUser(RegisterUserDto userDto)
    {
        var newUser = await userRepo.RegisterUserAsync(userDto); 
        return newUser;
    }

    public async Task<(UserDto? user, string? error)> LoginUser(LoginUserDto userDto)
    {
        var loginResult = await userRepo.LoginAsync(userDto);
        if (loginResult.Error != null) return (null, loginResult.Error);
        return (loginResult.User, null);
    }
}