using Domain.Entities;
using Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IStreamingService
    {
        Task<string> GetUserIdByAccessToken(string accessToken, CancellationToken cancellationToken);
        Task<User> GetCurrentUser(string accessToken, CancellationToken cancellationToken);
        Task SortPlaylistAsync(string playlistId, SortDirection direction, CancellationToken cancellationToken);
        Task RemoveOrArchiveTracksAsync(string playlistId, int maximumTracks, string archiveListId = "");
    }
}
