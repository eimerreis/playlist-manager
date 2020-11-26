using Application.Common.Interfaces;
using FluentAssertions;
using Infrastructure.Streaming;
using Microsoft.Extensions.Configuration;
using Moq;
using SpotifyAPI.Web;
using System.Linq;
using System.Threading;
using Xunit;

namespace Infrastructure.IntegrationTests
{

    public class SpotifyServiceTests
    {
        [Fact]
        public async void ShouldRemoveItems()
        {
            // Arrange
            var accessToken = "BQAqqcamWRPmlTQIXjJdU434qvufZ3FOp6yrgNTW38ug6YRz9PkI8b3RWT8oZREU2HNZDjUJGtBNs4t4omxa2oPWJteF9jvVVXoARVUkJKIj90zulM1qWeP8zxMd9ZSkJN9lSoxmbeO7U0eXbKcsKDodAXqyzaFGvdMgsHW3PbnWk34nBqzYlvMjlRTNmSY0jFJ_8jOEIPHh_rkMdmZDyZNqDlei_32nlyV93ZXwEyUIyMkM928mBBLpfIivnuE2Jx-2UXMQIMRpbs_rOq3p";
            var playlistId = "5qwobGpX8XWmsT9sdbX8ms";
            var archiveListId = "0nhkQH0NluA1M8sfXJKP8n";
            var maximumTracks = 5;

            var userService = new Mock<ICurrentUserService>();
            var tokenService = new Mock<IUserTokenService>();
            var spotifyConfig = SpotifyClientConfig.CreateDefault();
            var spotifyClient = new SpotifyClient(spotifyConfig.WithToken(accessToken));
            var spotifyService = new SpotifyService(userService.Object, spotifyConfig, tokenService.Object);

            // Act
            await spotifyService.RemoveOrArchiveTracksAsync(playlistId, maximumTracks, archiveListId);
            var assertPlaylist = await spotifyClient.Playlists.Get(playlistId);

            // Assert
            assertPlaylist.Tracks.Items.Count.Should().Be(maximumTracks);
        }

        [Fact]
        public async void ShouldSortItemsDescending()
        {
            // Arrange
            var accessToken = "BQAqqcamWRPmlTQIXjJdU434qvufZ3FOp6yrgNTW38ug6YRz9PkI8b3RWT8oZREU2HNZDjUJGtBNs4t4omxa2oPWJteF9jvVVXoARVUkJKIj90zulM1qWeP8zxMd9ZSkJN9lSoxmbeO7U0eXbKcsKDodAXqyzaFGvdMgsHW3PbnWk34nBqzYlvMjlRTNmSY0jFJ_8jOEIPHh_rkMdmZDyZNqDlei_32nlyV93ZXwEyUIyMkM928mBBLpfIivnuE2Jx-2UXMQIMRpbs_rOq3p";
            var playlistId = "5qwobGpX8XWmsT9sdbX8ms";

            var userService = new Mock<ICurrentUserService>();
            var tokenService = new Mock<IUserTokenService>();

            var spotifyConfig = SpotifyClientConfig.CreateDefault();
            var spotifyClient = new SpotifyClient(spotifyConfig.WithToken(accessToken));
            var spotifyService = new SpotifyService(userService.Object, spotifyConfig, tokenService.Object);

            var beforePlaylist = await spotifyClient.Playlists.Get(playlistId);
            beforePlaylist.Tracks.Items.OrderByDescending(x => x.AddedAt);

            // Act
            await spotifyService.SortPlaylistAsync(playlistId, Domain.Enums.SortDirection.Descending, CancellationToken.None);

            var assertPlaylist = await spotifyClient.Playlists.Get(playlistId);

            // Assert
            // AddedDate should be the same 
            assertPlaylist.Tracks.Items[0].AddedAt.Should().Be(beforePlaylist.Tracks.Items[0].AddedAt);

        }
    }
}
