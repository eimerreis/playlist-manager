using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserTokenService
    {
        Task UpdateAccessToken(string userId, string accessToken, CancellationToken cancellationToken);
        Task<string> GetAccessToken(string userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetRefreshToken(string userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
