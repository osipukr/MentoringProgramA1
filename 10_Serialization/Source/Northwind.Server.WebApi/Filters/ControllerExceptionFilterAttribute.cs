using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Northwind.Server.BusinessLayer.Exceptions;

namespace Northwind.Server.WebApi.Filters
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Dictionary<ExceptionEventTypes, int> _statusCodesMapper =
            new Dictionary<ExceptionEventTypes, int>
            {
                [ExceptionEventTypes.InvalidParameters] = StatusCodes.Status400BadRequest,
                [ExceptionEventTypes.NotFound] = StatusCodes.Status404NotFound
            };

        public override void OnException(ExceptionContext context)
        {
            ExceptionHandler(context);

            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            ExceptionHandler(context);

            return base.OnExceptionAsync(context);
        }

        private void ExceptionHandler(ExceptionContext context)
        {
#if DEBUG
            var message = context.Exception.Message;
#else
            var message = "Something went wrong on the server...";
#endif
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is NorthwindException exception)
            {
                message = exception.Message;

                if (_statusCodesMapper.ContainsKey(exception.ExceptionEventType))
                {
                    statusCode = _statusCodesMapper[exception.ExceptionEventType];
                }
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = statusCode
            };
        }
    }
}