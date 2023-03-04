using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Common.Attributes.ActionLog;
public class LogActionAttribute : ActionFilterAttribute
{
    [ExcludeFromCodeCoverage]
    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    {
        Console.WriteLine("Action logged");
    }
}
