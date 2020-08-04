import { ManagementRequest, ManagementConfiguration } from "@eimerreis/playlist-manager-shared";
import SpotifyApi from "spotify-web-api-node";
import { Database } from "../data-access/Database";
import shortid from "shortid";
import logger from "loglevel";
import { InternalManagementRequest } from "../types/InternalManagementRequest";
import { Decrypt, Encrypt } from "../data-access/Crypto";


export const AddManagementJobs = async (managementRequest: ManagementRequest) => {
    try {
    // give the management job a unqiue id
    const internalRequest: InternalManagementRequest = { ...managementRequest, id: shortid() }

    // encrypt the refresh token
    internalRequest.refreshToken = Encrypt(internalRequest.refreshToken);

    // write management job to the database
    Database.get("managementJobs")
            .push(internalRequest)
            .write();
    } catch(err) {
        logger.error(err);
    }

}