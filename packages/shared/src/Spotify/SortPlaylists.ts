import loglevel from "loglevel";
import kleur from "kleur";
import SpotifyWebApi from "spotify-web-api-node";

import { ManagementConfiguration } from "../Types/ManagementConfiguration";
import { SortTracks } from "./SortTracks";
import { FetchPlaylistTracks } from "./FetchPlaylistTracks";
import { GetPlaylistSnapshotId } from "./GetCurrentPlaylistSnapshotId";

export const SortPlaylists = async (managementOptions: ManagementConfiguration[], api: SpotifyWebApi) => {
    for (let { playlist, direction } of managementOptions) {
        let tracks = await FetchPlaylistTracks(playlist.id, api);
        const orderByDate = SortTracks(tracks, direction).map(x => x.track);
        const tracksToMove = tracks.map(x => x.track.id);

        let snapshotId = await GetPlaylistSnapshotId(playlist.id, api);
        let snapshotCounter = 0;
        for (const trackId of tracksToMove) {
            // workaround, as it seems that the snapshot retention is maximum 12 items
            // after 12 order changes, we need to get the latest snapshot of the playlist
            if (snapshotCounter === 11) {
                console.debug(`Retrieving new Playlist State...`)
                console.debug(`${kleur.bold("OldSnapshotId:")} ${playlist.snapshot_id}`);
                snapshotId = await GetPlaylistSnapshotId(playlist.id, api);
                console.debug(`${kleur.bold("NewSnapshotId:")} ${playlist.snapshot_id}`);
                tracks = await FetchPlaylistTracks(playlist.id, api);
                snapshotCounter = 0;
            }

            const oldIndex = tracks.findIndex(x => x.track.id === trackId);
            const track = tracks[oldIndex];
            const newIndex = orderByDate.findIndex(x => x.id === trackId);

            if (oldIndex !== newIndex) {
                loglevel.info(`Moving track "${kleur.bold(track.track.name)}" from position ${kleur.bold(oldIndex)} to ${kleur.bold(newIndex)}`);

                // move from oldIndex to newIndex. It's essentiall to provide the snapshot_id of the playlist here
                // because otherwise, we'd have to update our newIndex after every change. See https://developer.spotify.com/documentation/general/guides/working-with-playlists/#version-control-and-snapshots
                await api.reorderTracksInPlaylist(playlist.id, oldIndex, newIndex, { snapshot_id: snapshotId });
                snapshotCounter += 1;
            }
        }
    }
}