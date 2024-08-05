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
        if (userDto.Money < 0)
            throw new InvalidEntryException("User has to have more than 0 of currency.");
        var user = userDto.FromDtoToUser();
        var userStatus = await userRepo.RegisterUserAsync(user, userDto.Password);
        if (!userStatus.Succeeded)
            throw new DefaultException(string.Join(", ", userStatus.Errors.ToList().Select(e => e.Description)));
        var roleStatus = await userRepo.RegisterRoleOnUserAsync(user);
        if (!roleStatus.Succeeded)
            throw new DefaultException(string.Join(", ", roleStatus.Errors.ToList().Select(e => e.Description)));
        var securityToken = tokenService.CreateToken(user);
        return user.FromUserToNewUser(securityToken);
    }

    public async Task<UserDto> LoginUser(LoginUserDto userDto)
    {
        var user = await userRepo.GetUserByUsername(userDto.Username);
        if (user == null)
            throw new NotFoundException("User does not exist!");
        var signInResult = await userRepo.LoginAsync(user, userDto.Password);
        if (!signInResult.Succeeded)
            throw new DefaultException("Wrong password!");
        var securityToken = tokenService.CreateToken(user);
        return user.FromUserToNewUser(securityToken);
    }

    public async Task<User> UpdateUser(UpdateUserDto userDto, string username)
    {
        var user = await userRepo.GetUserByUsername(username);
        if (user == null)
            throw new NotFoundException("User does not exist!");
        user.Money = userDto.Money;
        await userRepo.UpdateUser(user);
        return user;
    }
}