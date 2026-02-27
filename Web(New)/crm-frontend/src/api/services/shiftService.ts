import { apiClient } from "../config.ts";
import type {
    ShiftRequest,
    ShiftResponse,
    ShiftUpdateRequest
} from "../types/shift.ts";

export const shiftService = {
    getAll: async () => {
        return await apiClient.get<ShiftResponse[]>('/shifts');
    },

    getById: async (id: number) => {
        return await apiClient.get<ShiftResponse>(`/shifts/${id}`);
    },

    create: async (request: ShiftRequest) => {
        return await apiClient.post<ShiftResponse>('/shifts', request);
    },

    update: async (id: number, request: ShiftUpdateRequest) => {
        return await apiClient.put(`/shifts/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/shifts/${id}`);
    }
};