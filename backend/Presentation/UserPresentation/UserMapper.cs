using ExpenseControlApplication.Data.Entities;
using ExpenseControlApplication.Presentation.SpendingPresentation;

namespace ExpenseControlApplication.Presentation.UserPresentation;

public static class UserMapper
{
    //Data Type Object
    public static UserDto FromUserToNewUser(this User userDto, string token)
    {
        return new UserDto
        {
            Username = userDto.UserName!,
            Money = userDto.Money,
            TotalGot = userDto.TotalGot,
            Email = userDto.Email!,
            TotalSpent = userDto.TotalSpent,
            Token = token,
            Spendings = userDto.Spendings.Select(s => s.FromSpendingToDto()).ToList()
        };
    }
    
}