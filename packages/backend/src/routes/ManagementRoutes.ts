import express, { Response, Request} from "express";
import { ManagementConfiguration, ManagementRequest } from "@eimerreis/playlist-manager-shared";

export const RegisterManagementRoutes = () => {
    const router = express.Router();
    router.post("/", AddManagementJobs);

    return router;
}


export const AddManagementJobs = (req: Request<{}, any, ManagementRequest>, res: Response) => {
    const { configurations, refreshToken } = req.body;
    
}