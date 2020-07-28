import { Direction } from "./Direction";
export interface ManagementOption {
    playlist: SpotifyApi.PlaylistObjectSimplified;
    maxTracks: number;
    archive: boolean;
    direction: Direction;
}
