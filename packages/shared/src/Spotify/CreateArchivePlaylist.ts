import SpotifyApi from "spotify-web-api-node";
import logger from "loglevel";
import kleur from "kleur";
import { AddTracksToArchiveList } from "./AddTracksToArchiveList";

type CreateArchivePlaylistArgs = {
    playlist: SpotifyApi.PlaylistObjectSimplified,
    listName: string,
    accountId: string;
}

export const CreateArchivePlaylist = async ({ accountId, listName, playlist }: CreateArchivePlaylistArgs, api: SpotifyApi) => {
    try {
        logger.info(`Creating archive playlist ${listName}`);
        const archiveListResponse = await api.createPlaylist(accountId, listName, { public: false, description: `Archive for Playlist ${playlist.name}` });
        const archiveListid = archiveListResponse.body.id;

        AddTracksToArchiveList(playlist.id, archiveListid, api);
        return archiveListid;
    } catch (err) {
        logger.error(`Failed to create archive playlist ${kleur.bold(listName)}. Error: ${JSON.stringify(err)}`);
    }
}
