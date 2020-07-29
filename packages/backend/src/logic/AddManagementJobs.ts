import { ManagementRequest, ManagementConfiguration } from "@eimerreis/playlist-manager-shared";
import SpotifyApi from "spotify-web-api-node";
import { Database } from "../data-access/Database";
import shortid from "shortid";
import cron from "cron";
import { ExecuteManagementJob } from "./ExecuteManagementJob";


export const AddManagementJobs = async (managementRequest: ManagementRequest) => {
    // give the management job a unqiue id
    managementRequest = { ...managementRequest, id: shortid() }

    // write stuff to database
    Database.get("managementJobs")
            .push(managementRequest)
            .write();
}