using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Playlists.Commands.SortPlaylist
{
    public class SortPlaylistCommand: IRequest
    {
        public string PlaylistId { get; set; }
        public SortDirection Direction { get; set; }
        public string AccessToken { get; set; }
    }
}
