import { apiClient } from "../config.ts";
import type {
    PartSetFilter,
    PartSetRequest,
    PartSetResponse,
    PartSetUpdateRequest
} from "../types/partSet.ts";

export const partSetService = {
    getPaged: async (filter: PartSetFilter) => {
        return (await apiClient.get<PartSetResponse[]>('/part-sets', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<PartSetResponse>(`/part-sets/${id}`)).data;
    },

    getByOrderId: async (orderId: number) => {
        return (await apiClient.get<PartSetResponse[]>(`/part-sets/orders/${orderId}`)).data;
    },

    create: async (request: PartSetRequest) => {
        return (await apiClient.post<PartSetResponse>('/part-sets', request)).data;
    },

    update: async (id: number, request: PartSetUpdateRequest) => {
        return (await apiClient.put(`/part-sets/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/part-sets/${id}`)).data;
    }
};
