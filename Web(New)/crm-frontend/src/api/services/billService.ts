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
        return (await apiClient.get<BillResponse[]>('/bills', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<BillResponse>(`/bills/${id}`)).data;
    },

    fetchDebt: async (id: number) => {
        return (await apiClient.get<number>(`/bills/${id}/debt`)).data;
    },

    create: async (request: BillRequest) => {
        return (await apiClient.post<BillResponse>('/bills', request)).data;
    },

    update: async (id: number, request: BillUpdateRequest) => {
        return (await apiClient.put(`/bills/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/bills/${id}`)).data;
    }
};

export const billStatusService = {
    getAll: async () => {
        return (await apiClient.get<BillStatusResponse[]>('/bill-statuses')).data;
    }
};
