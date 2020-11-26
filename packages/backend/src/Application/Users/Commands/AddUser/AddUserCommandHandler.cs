using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IDatabaseContext _DatabaseContext;

        public AddUserCommandHandler(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _DatabaseContext.Users.AddAsync(new Domain.Entities.User()
            {
                Id = request.UserId,
                AccessToken = request.AccessToken,
                RefreshToken = request.RefreshToken
            }, cancellationToken);
            await _DatabaseContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
