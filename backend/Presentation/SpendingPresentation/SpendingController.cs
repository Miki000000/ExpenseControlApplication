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
        var userName = User.GetUsername()!;
        var spendingCreated = await spendingServices.CreateAsync(spendingDto, userName); 
        return spendingCreated == null ? NotFound("User not found") : Ok(spendingCreated);
    }
}