import express from "express";
import bodyParser from "body-parser";
import morgan from "morgan";
import logger from "loglevel";
import cron from "cron";
import kleur from "kleur";

import { RegisterRoutes } from "./routes";
import { JobList, Database } from "./data-access/Database";
import { ExecuteManagementJob } from "./logic/ExecuteManagementJob";

const app = express();
app.use(bodyParser.json());
app.use(morgan('combined'));

//todo: add authentication middleware, which checks if the access token provided
//is a spotify access token, for example by getting the /me route

app.use("/api", RegisterRoutes());

const RunJobs = () => {
    logger.info("Initially Registering Job Watcher");
    const jobWatcher = cron.job("1/5 * * * * *", () => {
        logger.info("Looking for new job registrations...");

        const runningJobs = JobList.get("jobs").value();
        const registeredJobs = Database.get("managementJobs").value();

        const notRunningJobs = registeredJobs.filter(x => x.id && !runningJobs.includes(x.id));
        logger.info(`Found ${notRunningJobs.length} new Jobs to schedule`);
        for(const job of notRunningJobs) {
            ExecuteManagementJob(job);
        }
    });
    jobWatcher.start();
}

app.listen(3001, () => {
    // make log level dependent on environment
    logger.setLevel("DEBUG");
    logger.info(`Management Service is running`);
    RunJobs();
})


