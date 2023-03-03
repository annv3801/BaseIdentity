using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Common.Attributes.ActionLog
{
    /// <summary>
    /// To Log Create, Update, Delete action of each user in the system.
    /// </summary>
    public class LogActionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// For Web API controllers
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        [ExcludeFromCodeCoverage]
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine("Action logged");
        }
    }
}