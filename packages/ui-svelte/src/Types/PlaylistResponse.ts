// Generated by https://quicktype.io
import { Playlist } from "./Playlist";

export interface PlaylistsResponse {
    href:     string;
    items:    Playlist[];
    limit:    number;
    next:     string;
    offset:   number;
    previous: null;
    total:    number;
}

export interface ExternalUrls {
    spotify: string;
}

export interface Image {
    height: number | null;
    url:    string;
    width:  number | null;
}

export interface Owner {
    display_name:  string;
    external_urls: ExternalUrls;
    href:          string;
    id:            string;
    type:          OwnerType;
    uri:           string;
}

export enum OwnerType {
    User = "user",
}

export interface Tracks {
    href:  string;
    total: number;
}

export enum ItemType {
    Playlist = "playlist",
}
