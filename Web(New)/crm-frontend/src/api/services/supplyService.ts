import { apiClient } from "../config.ts";
import type {
    SupplyFilter,
    SupplyRequest,
    SupplyResponse
} from "../types/supply.ts";

export const supplyService = {
    getPaged: async (filter: SupplyFilter) => {
        return await apiClient.get<SupplyResponse[]>('/supplies', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<SupplyResponse>(`/supplies/${id}`);
    },

    create: async (request: SupplyRequest) => {
        return await apiClient.post<SupplyResponse>('/supplies', request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/supplies/${id}`);
    }
};