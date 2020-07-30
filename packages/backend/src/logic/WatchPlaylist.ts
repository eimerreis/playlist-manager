import SpotifyApi from "spotify-web-api-node";
import { SpotifyClientId, SpotifyClientSecret } from "@env";
import logger from "loglevel";

import { ManagementConfiguration, SortPlaylists, FetchPlaylistTracks } from "@eimerreis/playlist-manager-shared";
import kleur from "kleur";
import { Decrypt } from "../data-access/Crypto";


export const WatchPlaylist = async (config: ManagementConfiguration, refreshToken: string) => {
    try {
        // decrypt the refresh token
        refreshToken = Decrypt(refreshToken);

        const api = new SpotifyApi({
            clientId: SpotifyClientId,
            clientSecret: SpotifyClientSecret,
            refreshToken
        })

        logger.info(`Starting with management for playlist ${kleur.bold(config.playlist.name)}`);
        // refresh the acessToken
        const { body } = await api.refreshAccessToken();
        api.setAccessToken(body.access_token);

        logger.info(`Sorting playlist...`);
        // sort playlist according to configuration
        await SortPlaylists([config], api);

        logger.info(`Removing obsolete tracks`);
        // put oldest track into archive list if archive is true
        await RemoveObsoleteTracks(config, api);
    } catch (err) {
        logger.error(err);
    }
}

const RemoveObsoleteTracks = async (config: ManagementConfiguration, api: SpotifyApi) => {
    try {
        const { archive, playlist, maxTracks } = config;

        const tracks = await FetchPlaylistTracks(playlist.id, api);
        const tracksToDelete = tracks.slice(maxTracks).map(x => x.track);

        logger.debug(`Tracks to delete ${kleur.bold(tracksToDelete.length)}`);

        if(tracksToDelete.length > 0) {
            if (archive) {
                logger.debug(`Adding tracks to archive playlist with id ${kleur.bold(archive)}`);
                // put oldest tracks to archive list
                api.addTracksToPlaylist(archive, tracksToDelete.map(x => x.uri));
            }
    
            api.removeTracksFromPlaylist(playlist.id, tracksToDelete.map(x => ({ uri: x.uri })));
        }
    } catch (err) {
        logger.error(err);
    }
}