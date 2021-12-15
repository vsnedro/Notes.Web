using System;
using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public Guid UserId => User.Identity.IsAuthenticated
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
            : Guid.Empty;

    }
}
