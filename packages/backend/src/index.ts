import express from "express";
import bodyParser from "body-parser";
import logger from "loglevel";
import cron from "cron";
import kleur from "kleur";

import { RegisterRoutes } from "./routes";
import { JobList, Database } from "./data-access/Database";

const app = express();
app.use(bodyParser.json());

//todo: add authentication middleware, which checks if the access token provided
//is a spotify access token, for example by getting the /me route

app.use("/api", RegisterRoutes);

const RunJobs = () => {
    cron.job("* * * * *", () => {
        logger.trace("Looking for new job registrations...");

        const runningJobs = JobList.get("jobs").value();
        const registeredJobs = Database.get("managementJobs").value();

        const notRunningJobs = registeredJobs.filter(x => x.id && !runningJobs.includes(x.id));
        for(const job of notRunningJobs) {
            
        }
    }, () => {

    })
}
