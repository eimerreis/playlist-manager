import SpotifyWebApi from "spotify-web-api-node";
import { FetchAllItems } from "./FetchAllItems";

export const FetchPlaylistTracks = (playlistId: string, api: SpotifyWebApi) => FetchAllItems((limit, offset) => api.getPlaylistTracks(playlistId, { limit, offset }));
