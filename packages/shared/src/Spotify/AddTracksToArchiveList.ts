import SpotifyApi from "spotify-web-api-node";
import logger from "loglevel";
import kleur from "kleur";
import { FetchPlaylistTracks } from "./FetchPlaylistTracks";

export const AddTracksToArchiveList = async (playlistId: string, archiveListId: string, api: SpotifyApi) => {
    try {
        logger.info(`Adding tracks to archive playlist`);
        // add all current tracks of the playlist to the archive list
        const tracks = await FetchPlaylistTracks(playlistId, api);
        api.addTracksToPlaylist(archiveListId, tracks.map(x => x.track.uri));
    }
    catch (err) {
        logger.error(`Failed to add tracks to archive playlist with id ${kleur.bold(archiveListId)}. Error: ${JSON.stringify(err)}`);
    }
};
