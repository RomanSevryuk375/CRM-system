import { apiClient } from "../config.ts";
import type {
    ShiftRequest,
    ShiftResponse,
    ShiftUpdateRequest
} from "../types/shift.ts";

export const shiftService = {
    getAll: async () => {
        return (await apiClient.get<ShiftResponse[]>('/shifts')).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<ShiftResponse>(`/shifts/${id}`)).data;
    },

    create: async (request: ShiftRequest) => {
        return (await apiClient.post<ShiftResponse>('/shifts', request)).data;
    },

    update: async (id: number, request: ShiftUpdateRequest) => {
        return (await apiClient.put(`/shifts/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/shifts/${id}`)).data;
    }
};
