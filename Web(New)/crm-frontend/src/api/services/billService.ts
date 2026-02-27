import { apiClient } from "../config.ts";
import type {
    BillFilter,
    BillRequest,
    BillResponse,
    BillStatusResponse,
    BillUpdateRequest
} from "../types/bill.ts";

export const billService = {
    getPaged: async (filter: BillFilter) => {
        return await apiClient.get<BillResponse[]>('/bills', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<BillResponse>(`/bills/${id}`);
    },

    fetchDebt: async (id: number) => {
        return await apiClient.get<number>(`/bills/${id}/debt`);
    },

    create: async (request: BillRequest) => {
        return await apiClient.post<BillResponse>('/bills', request);
    },

    update: async (id: number, request: BillUpdateRequest) => {
        return await apiClient.put(`/bills/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/bills/${id}`);
    }
};

export const billStatusService = {
    getAll: async () => {
        return await apiClient.get<BillStatusResponse[]>('/bill-statuses');
    }
};