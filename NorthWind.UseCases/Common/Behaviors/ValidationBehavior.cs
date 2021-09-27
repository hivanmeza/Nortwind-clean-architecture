using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace NorthWind.UseCases.Common.Behaviors
{
    public class ValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse>where TResponse : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> Validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)=>Validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var Failures = Validators
                .Select(v => v.Validate(request))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (Failures.Any())
            {
                throw new ValidationException(Failures);
            }
            return next();
        }
    }
}