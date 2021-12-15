using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Notes.Application.Services.User;

namespace Notes.WebApi.Services.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor) =>
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

        public Guid UserId
        {
            get
            {
                var id = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.NewGuid() : Guid.Parse(id);
            }
        }
    }
}
