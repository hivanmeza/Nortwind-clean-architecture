using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NorthWind.Entities.Exceptions;

namespace NorthWind.WebExceptionPresenter
{
    public class GeneralExceptionHandler:ExceptionHandlerBase,IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            var Exception = context.Exception as GeneralException;
            return SetResult(context, StatusCodes.Status500InternalServerError, Exception.Message, Exception.Detail);
        }
    }
}