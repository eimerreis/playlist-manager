using MediatR;

namespace Application.Playlists.Commands.RemoveAndArchiveTracks
{
    public class RemoveAndArchiveTracksCommand: IRequest
    {
        public string PlaylistId { get; set; }
        public int MaximumTracks { get; set; }
        public string ArchiveListId { get; set; }
    }
}
