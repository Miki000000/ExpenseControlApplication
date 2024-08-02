using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Business.Services;

public class UserServices(IUserRepository userRepo, ITokenService tokenService) 
    : IUserServices
{
    public async Task<UserDto> RegisterUser(RegisterUserDto userDto)
    {
        var newUser = await userRepo.RegisterUserAsync(userDto); 
        return newUser;
        var user = userDto.FromDtoToUser();
        var userStatus = await userRepo.RegisterUserAsync(user, userDto.Password);
        if (!userStatus.success)
            throw new DefaultException(userStatus.message!);
        var securityToken = tokenService.CreateToken(user);
        return user.FromUserToNewUser(securityToken);
    }

    public async Task<(UserDto? user, string? error)> LoginUser(LoginUserDto userDto)
    {
        var loginResult = await userRepo.LoginAsync(userDto);
        if (loginResult.Error != null) return (null, loginResult.Error);
        return (loginResult.User, null);
    }
}