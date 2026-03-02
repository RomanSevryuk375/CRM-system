import { apiClient } from "../config.ts";
import type {
    StorageCellRequest,
    StorageCellResponse,
    StorageCellUpdateRequest
} from "../types/storageCell.ts";

export const storageCellService = {
    getAll: async () => {
        return await apiClient.get<StorageCellResponse[]>('/storage-cells');
    },

    getById: async (id: number) => {
        return await apiClient.get<StorageCellResponse>(`/storage-cells/${id}`);
    },

    create: async (request: StorageCellRequest) => {
        return await apiClient.post<StorageCellResponse>('/storage-cells', request);
    },

    update: async (id: number, request: StorageCellUpdateRequest) => {
        return await apiClient.put(`/storage-cells/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/storage-cells/${id}`);
    }
};