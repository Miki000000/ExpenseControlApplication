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
        try
        {
            var newUser = await userServices.RegisterUser(userDto);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
    {
        try
        {
            var user = await userServices.LoginUser(userDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                InvalidEntryException => 400,
                _ => 500,
            };
            return StatusCode(statusCode, new { Message = ex.Message });
        }
    }
}