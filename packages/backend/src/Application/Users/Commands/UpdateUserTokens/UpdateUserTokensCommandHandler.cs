using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUserTokens
{
    public class UpdateUserTokensCommandHandler : IRequestHandler<UpdateUserTokensCommand>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public UpdateUserTokensCommandHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<Unit> Handle(UpdateUserTokensCommand request, CancellationToken cancellationToken)
        {
            var user = await _DatabaseContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            user.AccessToken = request.AccessToken;
            user.RefreshToken = request.RefreshToken;

            _DatabaseContext.Users.Update(user);
            await _DatabaseContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
