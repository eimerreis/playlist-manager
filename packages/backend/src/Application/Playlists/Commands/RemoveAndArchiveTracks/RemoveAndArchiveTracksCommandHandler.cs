using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Playlists.Commands.RemoveAndArchiveTracks
{
    public class RemoveAndArchiveTracksCommandHandler : IRequestHandler<RemoveAndArchiveTracksCommand>
    {
        private readonly IStreamingService _SpotifyService;

        public RemoveAndArchiveTracksCommandHandler(IStreamingService spotifyService)
        {
            _SpotifyService = spotifyService;
        }

        public async Task<Unit> Handle(RemoveAndArchiveTracksCommand request, CancellationToken cancellationToken)
        {
            await _SpotifyService.RemoveOrArchiveTracksAsync(request.PlaylistId, request.MaximumTracks, request.ArchiveListId);
            return Unit.Value;
        }
    }
}
