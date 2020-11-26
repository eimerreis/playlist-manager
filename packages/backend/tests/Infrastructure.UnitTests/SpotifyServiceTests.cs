using Application.Common.Interfaces;
using Infrastructure.Streaming;
using Moq;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using System;
using Xunit;

namespace Infrastructure.UnitTests
{
    public class SpotifyServiceTests
    {
        [Fact]
        public async void SkipRemovalIfLimitIsNotExceeded()
        {
            //Arrange
            var userService = new Mock<ICurrentUserService>();
            userService.SetupGet(x => x.UserId).Returns("eimerreis");
            var userTokenService = new Mock<IUserTokenService>();

            var spotifyClientMock = new Mock<SpotifyClient>();
            spotifyClientMock.Setup(x => x.Playlists.Get(It.IsAny<string>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<FullPlaylist>(""));

            var spotifyConfig = SpotifyClientConfig.CreateDefault();
            var spotifyService = new SpotifyService(userService.Object, spotifyConfig.WithToken(""), userTokenService.Object);

            //Act
            await spotifyService.RemoveOrArchiveTracksAsync("", 60, "");

            //Assert
            spotifyClientMock.Verify(x => x.Playlists.RemoveItems(It.IsAny<string>(), It.IsAny<PlaylistRemoveItemsRequest>()), Times.Never);
        }
    }
}
