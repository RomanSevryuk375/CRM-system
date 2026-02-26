import { apiClient } from "../config.ts";
import type {
    PartFilter,
    PartRequest,
    PartResponse,
    PartUpdateRequest
} from "../types/part.ts";

export const partService = {
    getPaged: async (filter: PartFilter) => {
        return await apiClient.get<PartResponse[]>('/parts', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<PartResponse>(`/parts/${id}`);
    },

    create: async (request: PartRequest) => {
        return await apiClient.post<PartResponse>('/parts', request);
    },

    update: async (id: number, request: PartUpdateRequest) => {
        return await apiClient.put(`/parts/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/parts/${id}`);
    }
};