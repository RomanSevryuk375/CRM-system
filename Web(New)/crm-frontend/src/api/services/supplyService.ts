import { apiClient } from "../config.ts";
import type {
    SupplyFilter,
    SupplyRequest,
    SupplyResponse
} from "../types/supply.ts";

export const supplyService = {
    getPaged: async (filter: SupplyFilter) => {
        return (await apiClient.get<SupplyResponse[]>('/supplies', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<SupplyResponse>(`/supplies/${id}`)).data;
    },

    create: async (request: SupplyRequest) => {
        return (await apiClient.post<SupplyResponse>('/supplies', request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/supplies/${id}`)).data;
    }
};
