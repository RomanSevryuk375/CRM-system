import { apiClient } from "../config.ts";
import type {
    PositionFilter,
    PositionResponse,
    PositionWithPartRequest,
    PositionUpdateRequest
} from "../types/position.ts";

export const positionService = {
    getPaged: async (filter: PositionFilter) => {
        return (await apiClient.get<PositionResponse[]>('/positions', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<PositionResponse>(`/positions/${id}`)).data;
    },

    createWithPart: async (request: PositionWithPartRequest) => {
        return (await apiClient.post<PositionResponse>('/positions/parts', request)).data;
    },

    update: async (id: number, request: PositionUpdateRequest) => {
        return (await apiClient.put(`/positions/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/positions/${id}`)).data;
    }
};
