using ExpenseControlApplication.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseControlApplication.Utils.Configurations;

public class ControllerExceptionsConfigurations 
    : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            InvalidEntryException => 400,
            NotFoundException => 404,
            _ => 500,
        };
        context.Result = new JsonResult(new { Message = context.Exception.Message })
        {
            StatusCode = statusCode
        };
        
        base.OnException(context);
    }
}