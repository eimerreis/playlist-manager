import kleur from "kleur";
import logger from "loglevel";
import prompts from "prompts";
import superagent from "superagent";
import SpotifyWebApi from "spotify-web-api-node";

import { FetchAllItems, SortPlaylists, ManagementRequest, CreateArchivePlaylist, AddTracksToArchiveList } from "@eimerreis/playlist-manager-shared"
import { ManagementConfiguration } from "@eimerreis/playlist-manager-shared";
import { Direction } from "@eimerreis/playlist-manager-shared";

enum ArchiveChoice {
    Create,
    Select
}

export const ConfigurePlaylistManagement = async (api: SpotifyWebApi) => {
    try {
        const accountResponse = await api.getMe();
        const accountId = accountResponse.body.id;

        const playlists = await FetchAllItems((limit, offset) => api.getUserPlaylists({ limit, offset }), { limit: 50 });

        // ask which playlists to manage
        const playlistsToManage: { playlists: SpotifyApi.PlaylistObjectSimplified[] } = await prompts({
            type: "autocompleteMultiselect",
            validate: (x) => Array.isArray(x) && x.length > 0 ? true : "You should at least select 1 playlist",
            name: "playlists",
            message: "Which playlists do you want to manage?",
            choices: playlists.map(x => ({
                title: x.name,
                value: x,
            }))
        }, { onCancel: () => process.exit(0) });

        const managementConfigurations: ManagementConfiguration[] = [];

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
                },
            ], { onCancel: () => process.exit(0) });

            let archiveList: string = "";
            if (archive) {
                const { archiveListId, createOrSelect } = await prompts([
                    {
                        name: "createOrSelect",
                        type: archive ? "select" : null,
                        message: "Do you want to use an existing List as the archive, or create a new one?",
                        choices: [
                            {
                                "title": "Create an archive list",
                                "value": ArchiveChoice.Create,
                            },
                            {
                                "title": "Select an existing list",
                                "value": ArchiveChoice.Select
                            }
                        ]
                    },
                    {
                        name: "archiveListId",
                        message: prev => prev === ArchiveChoice.Create ? "Provide a name for the archive list" : "Select an archive list",
                        type: prev => prev === ArchiveChoice.Create ? "text" : "select",
                        choices: playlists.filter(x => x.id !== playlist.id).map(x => {
                            return {
                                title: x.name,
                                value: x.id
                            }
                        }),
                    }
                ], { onCancel: () => process.exit(0) });

                // create archive list if necessary
                if (createOrSelect === ArchiveChoice.Create) {
                    const id = await CreateArchivePlaylist({ accountId, listName: archiveListId, playlist }, api);
                    if(id) {
                        archiveList = id;
                    }
                } else {
                    archiveList = archiveListId;
                    await AddTracksToArchiveList(playlist.id, archiveListId, api);
                }
            }


            managementConfigurations.push({
                // todo: only send necessary data to backend
                playlist: {
                    name: playlist.name,
                    id: playlist.id,
                },
                maxTracks: numberOfTracks,
                archive: archiveList,
                direction
            });
        }

        if (managementConfigurations.length === 0) {
            logger.info("You did not select any playlist to manage. Exiting the progam.");
            process.exit(0);
        }

        logger.info(`Initially sorting playlists according to configuration.`);
        await SortPlaylists(managementConfigurations, api);

        const managementRequest: ManagementRequest = {
            configurations: managementConfigurations,
            refreshToken: api.getRefreshToken()!,
            accountId: accountResponse.body.id,
        }

        logger.info(`Sending ${kleur.bold(managementConfigurations.length)} playlist configurations to management service...`);
        const response = await superagent
            .post("http://playlist-manager.azurewebsites.net/api/manage")
            .send(managementRequest);

        if (response.status === 201) {
            logger.info(`Management for your playlists has been scheduled successfully`);
        } else {
            logger.error("Something went wrong, when sending your playlist management request");
        }
    } catch (err) {
        console.error(err);
    }
}
