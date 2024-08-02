using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data;
using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Interfaces;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;
using ExpenseControlApplication.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControlApplication.Business.Services;

public class UserServices(IUserRepository userRepo, ITokenService tokenService) 
    : IUserServices
{
    public async Task<UserDto> RegisterUser(RegisterUserDto userDto)
    {
        var user = userDto.FromDtoToUser();
        var userStatus = await userRepo.RegisterUserAsync(user, userDto.Password);
        if (!userStatus.success)
            throw new DefaultException(userStatus.message!);
        var securityToken = tokenService.CreateToken(user);
        return user.FromUserToNewUser(securityToken);
    }

    public async Task<UserDto> LoginUser(LoginUserDto userDto)
    {
        var loginStatus = await userRepo.LoginAsync(userDto);
        if (loginStatus.Error != null) 
            throw new InvalidEntryException(loginStatus.Error);
        var user = loginStatus.User;
        return user!;
    }
}