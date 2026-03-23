import { apiClient } from "../config.ts";
import type {
    SpecializationRequest,
    SpecializationResponse,
    SpecializationUpdateRequest
} from "../types/specialization.ts";

export const specializationService = {
    getAll: async () => {
        return (await apiClient.get<SpecializationResponse[]>('/specializations')).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<SpecializationResponse>(`/specializations/${id}`)).data;
    },

    create: async (request: SpecializationRequest) => {
        return (await apiClient.post<SpecializationResponse>('/specializations', request)).data;
    },

    update: async (id: number, request: SpecializationUpdateRequest) => {
        return (await apiClient.put(`/specializations/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/specializations/${id}`)).data;
    }
};
