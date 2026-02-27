import { apiClient } from "../config.ts";
import type {
    TaxFilter,
    TaxRequest,
    TaxResponse,
    TaxUpdateRequest,
    TaxTypeResponse
} from "../types/tax.ts";

export const taxService = {
    getPaged: async (filter: TaxFilter) => {
        return await apiClient.get<TaxResponse[]>('/taxes', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<TaxResponse>(`/taxes/${id}`);
    },

    create: async (request: TaxRequest) => {
        return await apiClient.post('/taxes', request);
    },

    update: async (id: number, request: TaxUpdateRequest) => {
        return await apiClient.put(`/taxes/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/taxes/${id}`);
    }
};

export const taxTypeService = {
    getAll: async () => {
        return await apiClient.get<TaxTypeResponse[]>('/tax-types');
    }
};