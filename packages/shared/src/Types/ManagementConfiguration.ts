import { Direction } from "./Direction";

export interface ManagementConfiguration {
    playlist: SpotifyApi.PlaylistObjectSimplified;
    maxTracks: number;
    archive: string;
    direction: Direction;
}
