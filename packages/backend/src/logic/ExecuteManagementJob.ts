import { ManagementRequest } from "@eimerreis/playlist-manager-shared";

import cron from "cron";

export const ExecuteManagementJob = (request: ManagementRequest) => {
    const job = cron.job("* * * * *", () => {

    });
}