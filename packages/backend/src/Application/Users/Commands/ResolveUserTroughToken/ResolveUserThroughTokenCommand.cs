using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.ResolveUserTroughToken
{
    public class ResolveUserThroughTokenCommand: IRequest<User>
    {
        public string AccessToken { get; set; }
    }
}
