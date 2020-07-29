import { ManagementConfiguration } from "./ManagementConfiguration";

export interface ManagementRequest {
    accountId: string;
    configurations: ManagementConfiguration[];
    refreshToken: string;
}