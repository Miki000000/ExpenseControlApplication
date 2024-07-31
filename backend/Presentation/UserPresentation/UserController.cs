using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Data.Entities;
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
        return newUser != null ? Ok(newUser) : NotFound();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
    {
        var (user, error) = await userServices.LoginUser(userDto);
        return user == null ? StatusCode(500, error) : Ok(user);
    }
}