import { apiClient } from "../config.ts";
import type {
    WorkerFilter,
    WorkerRequest,
    WorkerResponse,
    WorkerWithUserRequest,
    WorkerUpdateRequest
} from "../types/worker.ts";

export const workerService = {
    getPaged: async (filter: WorkerFilter) => {
        return (await apiClient.get<WorkerResponse[]>('/workers', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<WorkerResponse>(`/workers/${id}`)).data;
    },

    create: async (request: WorkerRequest) => {
        return (await apiClient.post<WorkerResponse>('/workers', request)).data;
    },

    createWithUser: async (request: WorkerWithUserRequest) => {
        return (await apiClient.post<WorkerResponse>('/workers/user', request)).data;
    },

    update: async (id: number, request: WorkerUpdateRequest) => {
        return (await apiClient.put(`/workers/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/workers/${id}`)).data;
    }
};
