using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Notes.Application.Services.User;

using Serilog;

namespace Notes.Application.Common.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ICurrentUserService currentUserService) =>
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;

            Log.Information("Notes request: {Name} {@UserId} {@Request}", requestName, userId, request);

            var response = await next();

            return response;
        }
    }
}
