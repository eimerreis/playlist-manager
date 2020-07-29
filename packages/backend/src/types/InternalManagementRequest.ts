import { ManagementRequest } from "@eimerreis/playlist-manager-shared";

export interface InternalManagementRequest extends ManagementRequest {
    id: string;
}