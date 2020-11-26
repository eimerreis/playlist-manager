using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Models.Configuration;
using Api.Models.Request;
using Application.Users.Commands.CreateOrUpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _Mediator;
        private readonly SpotifyConfiguration _SpotifyConfiguration;

        public AuthenticationController(SpotifyConfiguration spotifyConfiguration, IMediator mediator)
        {
            _SpotifyConfiguration = spotifyConfiguration;
            _Mediator = mediator;
        }

        [HttpGet]
        [Route("api/authenticate")]
        public IActionResult AuthenticateUser()
        {
            var loginRequest = new LoginRequest(
                BuildRedirectUri(),
                _SpotifyConfiguration.ClientId,
                LoginRequest.ResponseType.Code)
            {
                Scope = new[]
                {
                    Scopes.PlaylistModifyPrivate, Scopes.PlaylistModifyPublic
                }
            };

            var uri = loginRequest.ToUri();
            return Redirect(uri.ToString());
        }

        [HttpPost]
        [Route("/api/token")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenRequest request, CancellationToken cancellationToken)
        {
            var response = await new OAuthClient().RequestToken(
                new AuthorizationCodeRefreshRequest(_SpotifyConfiguration.ClientId, _SpotifyConfiguration.ClientSecret, request.RefreshToken));

            return Ok(response);
        }

        [HttpGet]
        [Route("auth/callback")]
        public async Task<IActionResult> AuthenticationCallback([FromQuery] string code)
        {
            var response = await new OAuthClient().RequestToken(
                    new AuthorizationCodeTokenRequest(
                            _SpotifyConfiguration.ClientId,
                            _SpotifyConfiguration.ClientSecret,
                            code, BuildRedirectUri()));
            await _Mediator.Send(new CreateOrUpdateUserCommand { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken });
            Response.Headers.Add("Authorization", $"Bearer {response.AccessToken}");
            return Redirect($"https://localhost:3000/jobs?accessToken={Uri.EscapeUriString(response.AccessToken)}&refreshToken=${Uri.EscapeUriString(response.RefreshToken)}");
        }

        private Uri BuildRedirectUri()
        {
            return new Uri($"{_SpotifyConfiguration.RedirectHost}/auth/callback");
        }
    }
}
