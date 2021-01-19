import { ExternalUrls } from "./PlaylistResponse";
import { Image, Owner, Tracks, ItemType } from "./PlaylistResponse";

export interface Playlist {
    collaborative: boolean;
    description:   string;
    external_urls: ExternalUrls;
    href:          string;
    id:            string;
    images:        Image[];
    name:          string;
    owner:         Owner;
    primary_color: null;
    public:        boolean;
    snapshot_id:   string;
    tracks:        Tracks;
    type:          ItemType;
    uri:           string;
}