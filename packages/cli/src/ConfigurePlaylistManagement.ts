import kleur from "kleur";
import prompts from "prompts";
import signale from "signale";

import SpotifyWebApi from "spotify-web-api-node";
import { FetchAllItems } from "./Spotify/FetchAllItems";
import { ManagementOption } from "./Types/ManagementOption";
import { Direction } from "./Types/Direction";
import { SortTracks } from "./SortTracks";
import { SortPlaylists } from "./Spotify/SortPlaylists";


export const ConfigurePlaylistManagement = async (api: SpotifyWebApi) => {
    try {
        const playlists = await FetchAllItems((limit, offset) => api.getUserPlaylists({ limit, offset }), { limit: 50 });

        // ask which playlists to manage
        const playlistsToManage: { playlists: SpotifyApi.PlaylistObjectSimplified[] } = await prompts({
            type: "multiselect",
            validate: (x) => Array.isArray(x) && x.length > 0 ? true : "You should at least select 1 playlist",
            name: "playlists",
            message: "Which playlists do you want to manage?",
            choices: playlists.map(x => ({
                title: x.name,
                value: x,
            }))
        }, { onCancel: () => process.exit(0) });

        const managementOptions: ManagementOption[] = [];

        // for each playlist, ask for managing options
        for (const playlist of playlistsToManage.playlists) {
            console.log("");
            console.log(kleur.bold().blue(`Management Settings for Playlist ${playlist.name}`));
            
            const { numberOfTracks, archive, direction } = await prompts([
                {
                    name: "numberOfTracks",
                    type: "number",
                    message: "How many Tracks should the playlist contain?",
                    initial: 60,
                },
                {
                    name: "direction",
                    type: "select",
                    message: "In which direction, shall the tracks be sorted (by date added)?",
                    choices: [
                        {
                            title: "Descending",
                            value: Direction.Descending
                        },
                        {
                            title: "Ascending",
                            value: Direction.Ascending
                        },
                    ],
                },
                {
                    name: "archive",
                    type: "toggle",
                    message: "Shall we archive removed tracks?",
                    active: "yes",
                    inactive: "no",
                    initial: true
                }
            ], { onCancel: () => process.exit(0) });

            managementOptions.push({
                playlist,
                maxTracks: numberOfTracks,
                archive,
                direction
            });
        }

        SortPlaylists(managementOptions, api);

        // todo: merge existing configuration!?

        // todo: start playlist manager command, which does the api call
        // persistence for access tokens?
    } catch (err) {
        console.error(err);
    }
}