using Api.Services.Extensions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        public string UserId => _HttpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value.ToString();
    }
}
