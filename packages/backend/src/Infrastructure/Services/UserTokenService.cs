using Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IDatabaseContext _DatabaseContext;

        public UserTokenService(IDatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public async Task<string> GetAccessToken(string userId, CancellationToken cancellationToken)
        {
            var user = await _DatabaseContext.Users.FindAsync(new[] { userId }, cancellationToken);
            return user.AccessToken;
        }

        public async Task<string> GetRefreshToken(string userId, CancellationToken cancellationToken)
        {
            var user = await _DatabaseContext.Users.FindAsync(new[] { userId }, cancellationToken);
            return user.RefreshToken;
        }

        public async Task UpdateAccessToken(string userId, string accessToken, CancellationToken cancellationToken)
        {
            var user = await _DatabaseContext.Users.FindAsync(userId);
            user.AccessToken = accessToken;
            await _DatabaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
