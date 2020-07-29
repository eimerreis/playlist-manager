import SpotifyApi from "spotify-web-api-node";
import SpotifyWebApi from "spotify-web-api-node";

export const GetPlaylistSnapshotId = async (playlistId: string, api: SpotifyWebApi) => {
    const { body } = await api.getPlaylist(playlistId, { fields: "snapshot_id"});
    return body.snapshot_id;
}