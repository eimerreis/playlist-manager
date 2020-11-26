using Application.Users.Commands.ResolveUserTroughToken;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SpotifyAuthenticationOptions: AuthenticationSchemeOptions
    {

    }

    public class SpotifyAuthenticationHandler : AuthenticationHandler<SpotifyAuthenticationOptions>
    {
        private readonly string _AuthorizationHeaderKey = "Authorization";
        private readonly IMediator _Mediator;

        public SpotifyAuthenticationHandler(
            IOptionsMonitor<SpotifyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IMediator mediator
            ) : base(options, logger, encoder, clock)
        {
            _Mediator = mediator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(_AuthorizationHeaderKey))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers[_AuthorizationHeaderKey];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var user = await _Mediator.Send(new ResolveUserThroughTokenCommand { AccessToken = token });
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Id),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
