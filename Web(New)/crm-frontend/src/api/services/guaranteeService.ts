import { apiClient } from "../config.ts";
import type {
    GuaranteeFilter,
    GuaranteeRequest,
    GuaranteeResponse,
    GuaranteeUpdateRequest
} from "../types/guarantee.ts";

export const guaranteeService = {
    getPaged: async (filter: GuaranteeFilter) => {
        return await apiClient.get<GuaranteeResponse[]>('/guarantees', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<GuaranteeResponse>(`/guarantees/${id}`);
    },

    create: async (request: GuaranteeRequest) => {
        return await apiClient.post<GuaranteeResponse>('/guarantees', request);
    },

    update: async (id: number, request: GuaranteeUpdateRequest) => {
        return await apiClient.put(`/guarantees/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/guarantees/${id}`);
    }
};