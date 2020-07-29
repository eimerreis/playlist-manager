import express from "express";

import { RegisterManagementRoutes } from "./ManagementRoutes";

export const RegisterRoutes = () => {
    const router = express.Router();
    router.use("/manage", RegisterManagementRoutes);
    return router;
}