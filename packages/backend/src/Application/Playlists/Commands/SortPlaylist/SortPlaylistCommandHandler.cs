using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Playlists.Commands.SortPlaylist
{
    public class SortPlaylistCommandHandler : IRequestHandler<SortPlaylistCommand>
    {
        private readonly IStreamingService _SpotifyService;

        public SortPlaylistCommandHandler(IStreamingService spotifyService)
        {
            _SpotifyService = spotifyService;
        }

        public async Task<Unit> Handle(SortPlaylistCommand request, CancellationToken cancellationToken)
        {
            await _SpotifyService.SortPlaylistAsync(request.PlaylistId, request.Direction, cancellationToken);
            return Unit.Value;
        }
    }
}
