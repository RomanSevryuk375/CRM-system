import { apiClient } from "../config.ts";
import type {
    WorkFilter,
    WorkRequest,
    WorkResponse,
    WorkUpdateRequest
} from "../types/work.ts";

export const workService = {
    getPaged: async (filter: WorkFilter) => {
        return await apiClient.get<WorkResponse[]>('/works', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<WorkResponse>(`/works/${id}`);
    },

    create: async (request: WorkRequest) => {
        return await apiClient.post<WorkResponse>('/works', request);
    },

    update: async (id: number, request: WorkUpdateRequest) => {
        return await apiClient.put(`/works/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/works/${id}`);
    }
};