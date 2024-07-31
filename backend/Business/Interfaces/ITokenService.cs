using ExpenseControlApplication.Data.Entities;

namespace ExpenseControlApplication.Business.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}