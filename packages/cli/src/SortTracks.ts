import { Direction } from "./Types/Direction";

export const SortTracks = (tracks: SpotifyApi.PlaylistTrackObject[], direction: Direction) => [...tracks].sort((a, b) => {
    const aDate = new Date(a.added_at);
    const bDate = new Date(b.added_at);

    return direction === Direction.Descending ? bDate.getTime() - aDate.getTime() : aDate.getTime() - bDate.getTime();
});
