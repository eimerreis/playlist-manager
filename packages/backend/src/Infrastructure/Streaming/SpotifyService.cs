using Api.Models.Configuration;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Polly;
using Polly.Retry;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Streaming
{
    public class SpotifyService : IStreamingService
    {
        private readonly ICurrentUserService _UserService;
        private readonly IUserTokenService _TokenService;
        private readonly SpotifyClientConfig _ClientConfig;
        private readonly SpotifyConfiguration _Configuration;
        private SpotifyClient _SpotifyClient;
        private readonly bool _IsInTest;

        private readonly AsyncRetryPolicy _TokenRefreshPolicy;

        /// <summary>
        /// Constructor used for unit testing
        /// </summary>
        public SpotifyService(SpotifyClient spotifyClient)
        {
            _SpotifyClient = spotifyClient;
            _IsInTest = true;
        }

        private AsyncRetryPolicy CreateRetryPolicy()
        {
            return Policy.Handle<APIUnauthorizedException>().RetryAsync(retryCount: 1, onRetry: async (exception, count) =>
            {
                //todo: pass through cancellationtoken
                var userId = _UserService.UserId;
                var refreshToken = await _TokenService.GetRefreshToken(userId, CancellationToken.None);
                var newTokenResponse = await new OAuthClient().RequestToken(
                    new AuthorizationCodeRefreshRequest(_Configuration.ClientId, _Configuration.ClientSecret, refreshToken));

                await _TokenService.UpdateAccessToken(userId, newTokenResponse.AccessToken, CancellationToken.None);
            });
        }

        public SpotifyService(ICurrentUserService userService, SpotifyClientConfig spotifyClientConfig, IUserTokenService tokenService)
        {
            _UserService = userService;
            _ClientConfig = spotifyClientConfig;
            _TokenService = tokenService;
            _TokenRefreshPolicy = CreateRetryPolicy();
        }

        public SpotifyService(string accessToken)
        {
            _SpotifyClient = CreateSpotifyClient(accessToken);
        }

        public async Task RemoveOrArchiveTracksAsync(string playlistId, int maximumTracks, string archiveListId = "")
        {
            await _TokenRefreshPolicy.ExecuteAsync(async () =>
            {
                //todo: how to inject spotifyClient in a cool way for unit testing?
                var spotifyClient = _IsInTest ? _SpotifyClient : await CreateSpotifyClient();
                var playlist = await spotifyClient.Playlists.Get(playlistId);
                var allTracks = await spotifyClient.PaginateAll(playlist.Tracks);

                // if limit is not exceeded, no need to remove or archive tracks
                if (playlist.Tracks.Items.Count <= maximumTracks)
                {
                    return;
                }

                // move tracks if archive list has been provided
                if (!string.IsNullOrWhiteSpace(archiveListId))
                {
                    var archiveList = await spotifyClient.Playlists.Get(archiveListId);
                    var trackUris = allTracks.Select((x, i) => new { x, i }).Where(x => x.i >= (maximumTracks - 1)).Select(x => (x.x.Track as FullTrack).Uri);

                    // you only can add 100 items per request, therefore we need to make separate api calls
                    // with each api call containing maximum 100 items
                    for (var i = 0; i < trackUris.Count(); i += 100)
                    {
                        await spotifyClient.Playlists.AddItems(archiveListId, new PlaylistAddItemsRequest(trackUris.Skip(i).Take(100).ToList()) { Position = 0 });
                    }
                }

                // remove tracks
                var tracksToRemove = allTracks.Select((x, i) => i).Where(i => i >= (maximumTracks));
                await spotifyClient.Playlists.RemoveItems(playlistId, new PlaylistRemoveItemsRequest { Positions = tracksToRemove.ToList(), SnapshotId = playlist.SnapshotId });
            });
        }

        public async Task SortPlaylistAsync(string playlistId, SortDirection direction, CancellationToken cancellationToken)
        {
            await _TokenRefreshPolicy.ExecuteAsync(async () =>
            {
                var spotifyClient = _IsInTest ? _SpotifyClient : await CreateSpotifyClient();
                var playlist = await spotifyClient.Playlists.Get(playlistId);
                var tracks = (await spotifyClient.PaginateAll(playlist.Tracks));
                var sortedTracks = direction == SortDirection.Descending ? tracks.OrderByDescending(x => x.AddedAt) : tracks.OrderBy(x => x.AddedAt);

                foreach (var track in sortedTracks)
                {
                    if (track.Track is FullTrack fullTrack)
                    {
                        var currentState = await spotifyClient.PaginateAll((await spotifyClient.Playlists.Get(playlistId)).Tracks);
                        var currentIndex = currentState.ToList().FindIndex(x => (x.Track as FullTrack).Uri == fullTrack.Uri);
                        var newIndex = sortedTracks.ToList().IndexOf(track);

                        // check if index of the current song needs to be changed
                        if (currentIndex != newIndex)
                        {
                            await spotifyClient.Playlists.ReorderItems(playlistId, new PlaylistReorderItemsRequest(currentIndex, newIndex));
                        }
                    }
                }
            });
        }

        public async Task<string> GetUserIdByAccessToken(string accessToken, CancellationToken cancellationToken)
        {
            var spotifyClient = _IsInTest ? _SpotifyClient : CreateSpotifyClient(accessToken);
            return (await spotifyClient.UserProfile.Current()).Id;
        }

        public async Task<User> GetCurrentUser(string accessToken, CancellationToken cancellationToken)
        {
            var spotifyClient = _IsInTest ? _SpotifyClient : CreateSpotifyClient(accessToken);
            var spotifyUser = await spotifyClient.UserProfile.Current();
            return new User() { AccessToken = accessToken, Id = spotifyUser.Id };
        }

        private SpotifyClient CreateSpotifyClient(string accessToken)
        {
            return new SpotifyClient(_ClientConfig.WithToken(accessToken));
        }

        private async Task<SpotifyClient> CreateSpotifyClient()
        {
            return new SpotifyClient(_ClientConfig.WithToken(await _TokenService.GetAccessToken(_UserService.UserId)));
        }
    }
}
