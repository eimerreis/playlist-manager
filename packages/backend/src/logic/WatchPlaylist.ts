import SpotifyApi from "spotify-web-api-node";
import { SpotifyClientId, SpotifyClientSecret } from "@env";

import { ManagementConfiguration, SortPlaylists, FetchPlaylistTracks, SortTracks } from "@eimerreis/playlist-manager-shared";


export const WatchPlaylist = (config: ManagementConfiguration, refreshToken: string) => {
    const api = new SpotifyApi({
        clientId: SpotifyClientId,
        clientSecret: SpotifyClientSecret,
        refreshToken
    })

    // sort playlist according to configuration
    SortPlaylists([config], api);

    // put oldest track into archive list if archive is true
    RemoveObsoleteTracks(config, api);
}

const RemoveObsoleteTracks = async (config: ManagementConfiguration, api: SpotifyApi) => {
    const { archive, playlist, maxTracks, direction } = config;

    const tracks = await FetchPlaylistTracks(playlist.id, api);
    const tracksToDelete = tracks.slice(maxTracks - 1).map(x => x.track);

    if(archive) {
        // put oldest tracks to archive list
    }

    api.removeTracksFromPlaylist(playlist.id, tracksToDelete, )
}