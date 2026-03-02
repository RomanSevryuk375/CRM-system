import { apiClient } from "../config.ts";
import type {
    SupplierRequest,
    SupplierResponse,
    SupplierUpdateRequest
} from "../types/supplier.ts";

export const supplierService = {
    getAll: async () => {
        return await apiClient.get<SupplierResponse[]>('/suppliers');
    },

    getById: async (id: number) => {
        return await apiClient.get<SupplierResponse>(`/suppliers/${id}`);
    },

    create: async (request: SupplierRequest) => {
        return await apiClient.post<SupplierResponse>('/suppliers', request);
    },

    update: async (id: number, request: SupplierUpdateRequest) => {
        return await apiClient.put(`/suppliers/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/suppliers/${id}`);
    }
};