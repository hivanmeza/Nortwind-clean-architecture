using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NorthWind.WebExceptionPresenter
{
    public interface IExceptionHandler
    {
        Task Handle(ExceptionContext context);
    }
}