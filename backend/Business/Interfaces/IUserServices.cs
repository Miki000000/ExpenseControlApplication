using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Data.Repositories;
using ExpenseControlApplication.Presentation.UserPresentation;

namespace ExpenseControlApplication.Business.Interfaces;

public class LoginResult
{
    public UserDto? User { get; set; }
    public string? Error { get; set; }
}

public interface IUserServices
{
    public Task<UserDto> RegisterUser(RegisterUserDto userDto);
    public Task<UserDto> LoginUser(LoginUserDto userDto);
    public Task<User> UpdateUser(UpdateUserDto userDto, string username);
}