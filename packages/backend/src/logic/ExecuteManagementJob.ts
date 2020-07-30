import cron from "cron";
import logger from "loglevel";
import kleur from "kleur";

import { PlaylistWatchInterval } from "@env";

import { WatchPlaylist } from "./WatchPlaylist";
import { JobList } from "../data-access/Database";
import { InternalManagementRequest } from "../types/InternalManagementRequest";

export const ExecuteManagementJob = (request: InternalManagementRequest) => {
    const job = cron.job(PlaylistWatchInterval, () => {
        logger.info(`Watching request with id ${kleur.bold(request.id)}`);
        for(const config of request.configurations) {
            WatchPlaylist(config, request.refreshToken);
        }
    }, () => LogComplete(request.configurations.map(x => x.playlist.name)));

    job.start();
    logger.info(`Started management job for request with id ${kleur.bold(request.id)}`);
    JobList.get("jobs").push(request.id).write();
}

export const LogComplete = (playlists: string[]) => {
    logger.info("Completed Management for Playlists:");
    playlists.forEach(list => logger.info(kleur.bold(list)));
}