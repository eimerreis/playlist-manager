import low from "lowdb";
import FileSync from "lowdb/adapters/FileSync";
import Memory from "lowdb/adapters/Memory";
import { InternalManagementRequest } from "../types/InternalManagementRequest";

interface Database {
    managementJobs: InternalManagementRequest[];
}

const adapter = new FileSync<Database>("db.json");
export const Database = low(adapter);

Database.defaults({ 
    managementJobs: [],
}).write();

interface JobDatabase {
    jobs: string[]
}

const memoryAdapter = new Memory<JobDatabase>("jobs.json");
export const JobList = low(memoryAdapter);

JobList.defaults({
    jobs: []
}).write();