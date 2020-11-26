using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.ResolveUserTroughToken
{
    public class ResolveUserThroughTokenCommandHandler : IRequestHandler<ResolveUserThroughTokenCommand, User>
    {
        private readonly IStreamingService _StreamingService;

        public ResolveUserThroughTokenCommandHandler(IStreamingService streamingService)
        {
            _StreamingService = streamingService;
        }

        public async Task<User> Handle(ResolveUserThroughTokenCommand request, CancellationToken cancellationToken)
        {
            return await _StreamingService.GetCurrentUser(request.AccessToken, cancellationToken);
        }
    }
}
