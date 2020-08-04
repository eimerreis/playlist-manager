import cron from "cron";
import logger from "loglevel";
import kleur from "kleur";

import { PlaylistWatchInterval } from "@env";

import { WatchPlaylist } from "./WatchPlaylist";
import { JobList, Database } from "../data-access/Database";
import { InternalManagementRequest } from "../types/InternalManagementRequest";

export const ExecuteManagementJob = (request: InternalManagementRequest) => {
    try {
        const job = cron.job(PlaylistWatchInterval, () => {
            for(const config of request.configurations) {
                const isInProgress = Database.get("inProgress").filter(x => x === config.playlist.id).value().length > 0;
                if(!isInProgress) {
                    logger.info(`Watching Playlist ${config.playlist.name}`);
                    WatchPlaylist(config, request.refreshToken);
                } else {
                    logger.info(`Management of playlist ${config.playlist.name} is still in progress...`);
                }
            }
        }, () => LogComplete(request.configurations.map(x => x.playlist.name)));
    
        job.start();
        logger.info(`Started management job for request with id ${kleur.bold(request.id)}`);
        JobList.get("jobs").push(request.id).write();
    } catch(err) {
        logger.error(err);
    }

}

export const LogComplete = (playlists: string[]) => {
    logger.info("Completed Management for Playlists:");
    playlists.forEach(list => logger.info(kleur.bold(list)));
}