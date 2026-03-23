import { apiClient } from "../config.ts";
import type {
    PartFilter,
    PartRequest,
    PartResponse,
    PartUpdateRequest
} from "../types/part.ts";

export const partService = {
    getPaged: async (filter: PartFilter) => {
        return (await apiClient.get<PartResponse[]>('/parts', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<PartResponse>(`/parts/${id}`)).data;
    },

    create: async (request: PartRequest) => {
        return (await apiClient.post<PartResponse>('/parts', request)).data;
    },

    update: async (id: number, request: PartUpdateRequest) => {
        return (await apiClient.put(`/parts/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/parts/${id}`)).data;
    }
};
