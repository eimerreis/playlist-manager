import express, { Response, Request } from "express";
import * as yup from "yup";
import { ManagementConfiguration, ManagementRequest } from "@eimerreis/playlist-manager-shared";

import { AddManagementJobs } from "../logic/AddManagementJobs";
import { ValidationError } from "../types/ValidationError";

export const RegisterManagementRoutes = () => {
    const router = express.Router();
    router.post("/", AddManagement);
    return router;
}


const RequestSchema = yup.object<ManagementRequest>().shape({
    accountId: yup.string().required(),
    configurations: yup.array().required().min(1),
    refreshToken: yup.string().required(),
});

const AddManagement = async (req: Request<{}, any, ManagementRequest>, res: Response) => {
    try {
        await RequestSchema.validate(req.body);
        AddManagementJobs(req.body);
        res.send(201);
    } catch (err) {
        const response: ValidationError = {
            message: "You provided an invalid payload",
            validationResult: err,
        }
        res.status(400);
        res.send(response);
    }
}