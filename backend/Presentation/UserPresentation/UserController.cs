using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Utils.Exceptions;
using ExpenseControlApplication.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControlApplication.Presentation.UserPresentation;

[Route("api/account")]
[ApiController]
public class UserController(IUserServices userServices) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
    {
        var newUser = await userServices.RegisterUser(userDto);
        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
    {
        var user = await userServices.LoginUser(userDto);
        return Ok(user);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
    {
        return Ok(await userServices.UpdateUser(userDto, User.GetUsername()));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var userName = User.GetUsername();
        var user = await userServices.GetUserByUsername(userName);
        return Ok(user);
    }

    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetAllUsersInfo()
    {
        var userName = User.GetUsername();
        var users = await userServices.GetAllUsers(userName);
        return Ok(users);
    }
}