using ExpenseControlApplication.Business.Interfaces;
using ExpenseControlApplication.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControlApplication.Presentation.SpendingPresentation;

[Route("api/spending")]
[ApiController]
public class SpendingController(ISpendingServices spendingServices) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] SpendingDto spendingDto)
    {
        var userName = User.GetUsername();
        var spendingCreated = await spendingServices.CreateAsync(spendingDto, userName); 
        return Ok(spendingCreated);
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var userName = User.GetUsername();
        var userSpendings = await spendingServices.GetAllUserSpendingAsync(userName);
        return Ok();
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var userName = User.GetUsername();
        var spending = await spendingServices.GetUserSpendingAsync(userName, id);
        return Ok(spending);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSpendingDto spendingDto)
    {
        var userName = User.GetUsername();
        var newSpending = await spendingServices.UpdateAsync(spendingDto, userName);
        return Ok(newSpending);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAll()
    {
        var userName = User.GetUsername();
        var deletedSpendings = await spendingServices.DeleteAllAsync(userName);
        return Ok(new {Message = $"Deleted all spendings successfully:\n {deletedSpendings}"});
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {
        var userName = User.GetUsername();
        var spendingDeleted = await spendingServices.DeleteAsync(id, userName);
        return Ok(spendingDeleted);
    }
}