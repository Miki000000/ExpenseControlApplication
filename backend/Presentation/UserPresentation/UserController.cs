using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Utils.Exceptions;
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
}