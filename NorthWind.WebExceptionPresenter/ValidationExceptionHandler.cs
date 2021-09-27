using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NorthWind.Entities.Exceptions;

namespace NorthWind.WebExceptionPresenter
{
    public class ValidationExceptionHandler : ExceptionHandlerBase, IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            var Exception = context.Exception as ValidationException;

            StringBuilder Builder = new StringBuilder();
            foreach (var Failure in Exception.Errors)
            {
                Builder.Append(
                    $"Propiedad: {Failure.PropertyName}, Error: {Failure.ErrorMessage}"
                );
            }

            return SetResult(context, StatusCodes.Status500InternalServerError, "Error de entrada.", Builder.ToString());

        }
    }
}