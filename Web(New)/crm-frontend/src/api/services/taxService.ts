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
        return (await apiClient.get<TaxResponse[]>('/taxes', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<TaxResponse>(`/taxes/${id}`)).data;
    },

    create: async (request: TaxRequest) => {
        return (await apiClient.post('/taxes', request)).data;
    },

    update: async (id: number, request: TaxUpdateRequest) => {
        return (await apiClient.put(`/taxes/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/taxes/${id}`)).data;
    }
};

export const taxTypeService = {
    getAll: async () => {
        return (await apiClient.get<TaxTypeResponse[]>('/tax-types')).data;
    }
};
