using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Commands.AddUser;
using Application.Users.Commands.UpdateUserTokens;
using Application.Users.Queries;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateOrUpdateUser
{
    public class CreateOrUpdateUserCommandHandler : IRequestHandler<CreateOrUpdateUserCommand>
    {
        private readonly IStreamingService _StreamingService;
        private readonly IMediator _Mediator;

        public CreateOrUpdateUserCommandHandler(IStreamingService streamingService, IMediator mediator)
        {
            _StreamingService = streamingService;
            _Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateOrUpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _StreamingService.GetUserIdByAccessToken(request.AccessToken, cancellationToken);

            try
            {
                var user = await _Mediator.Send<User>(new GetUserByIdQuery { UserId = userId });
                await _Mediator.Send(new UpdateUserTokensCommand { AccessToken = request.AccessToken, RefreshToken = request.RefreshToken, UserId = userId });
            } catch(UserNotFoundException)
            {
                await _Mediator.Send(new AddUserCommand { AccessToken = request.AccessToken, RefreshToken = request.RefreshToken, UserId = userId });
            }
            
            return Unit.Value;
        }
    }
}
