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
        return await apiClient.get<WorkerResponse[]>('/workers', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<WorkerResponse>(`/workers/${id}`);
    },

    create: async (request: WorkerRequest) => {
        return await apiClient.post<WorkerResponse>('/workers', request);
    },

    createWithUser: async (request: WorkerWithUserRequest) => {
        return await apiClient.post<WorkerResponse>('/workers/user', request);
    },

    update: async (id: number, request: WorkerUpdateRequest) => {
        return await apiClient.put(`/workers/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/workers/${id}`);
    }
};