using System.Threading;
using System.Threading.Tasks;
using Api.Models.Request;
using Application.Common.Exceptions;
using Application.Users.Commands.AddUser;
using Application.Users.Commands.UpdateUserTokens;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public UserController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        [Route("api/users")]
        [Authorize]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _Mediator.Send(command, cancellationToken);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [Route("api/users/{userId}")]
        public async Task<IActionResult> UpdateUserTokens([FromBody] UpdateUserTokensRequest request, [FromRoute] string userId, CancellationToken cancellationToken)
        {
            try
            {
                await _Mediator.Send(new UpdateUserTokensCommand
                {
                    AccessToken = request.AccessToken,
                    RefreshToken = request.RefreshToken,
                    UserId = userId
                }, cancellationToken);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
