using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Northwind.Server.BusinessLayer.Exceptions;

namespace Northwind.Server.WebApi.Filters
{
    public class ValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ActionHandler(context);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ActionHandler(context);
        }

        private static void ActionHandler(ActionContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var message = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .FirstOrDefault();

            throw new NorthwindException(message, ExceptionEventTypes.InvalidParameters);
        }
    }
}