import { apiClient } from "../config.ts";
import type {
    OrderFilter,
    OrderRequest,
    OrderResponse,
    OrderWithBillRequest,
    OrderUpdateRequest,
    OrderPatchRequest,
    OrderPriorityResponse,
    OrderStatusResponse
} from "../types/order.ts";

export const orderService = {
    getPaged: async (filter: OrderFilter) => {
        return (await apiClient.get<OrderResponse[]>('/orders', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<OrderResponse>(`/orders/${id}`)).data;
    },

    downloadPdf: async (id: number): Promise<string> => {
        const response = await apiClient.get(`/orders/${id}/pdf`, {
            responseType: 'blob'
        });
        return URL.createObjectURL(response.data);
    },

    create: async (request: OrderRequest) => {
        return (await apiClient.post<OrderResponse>('/orders', request)).data;
    },

    createWithBill: async (request: OrderWithBillRequest) => {
        return (await apiClient.post<OrderResponse>('/orders/bills', request)).data;
    },

    generatePdf: async (id: number) => {
        return (await apiClient.post<{ message: string, path: string }>(`/orders/${id}/pdf`)).data;
    },

    update: async (id: number, request: OrderUpdateRequest) => {
        return (await apiClient.put(`/orders/${id}`, request)).data;
    },

    patchStatus: async (id: number, request: OrderPatchRequest) => {
        return (await apiClient.patch(`/orders/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/orders/${id}`)).data;
    }
};

export const orderPriorityService = {
    getAll: async () => {
        return (await apiClient.get<OrderPriorityResponse[]>('/order-priorities')).data;
    }
};

export const orderStatusService = {
    getAll: async () => {
        return (await apiClient.get<OrderStatusResponse[]>('/order-statuses')).data;
    }
};
