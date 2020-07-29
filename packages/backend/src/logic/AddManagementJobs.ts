import { ManagementRequest, ManagementConfiguration } from "@eimerreis/playlist-manager-shared";
import SpotifyApi from "spotify-web-api-node";
import { Database } from "../data-access/Database";
import shortid from "shortid";
import cron from "cron";
import { ExecuteManagementJob } from "./ExecuteManagementJob";
import { InternalManagementRequest } from "../types/InternalManagementRequest";


export const AddManagementJobs = async (managementRequest: ManagementRequest) => {
    // give the management job a unqiue id
    const internalRequest: InternalManagementRequest = { ...managementRequest, id: shortid() }

    // write management job to the database
    Database.get("managementJobs")
            .push(internalRequest)
            .write();
}