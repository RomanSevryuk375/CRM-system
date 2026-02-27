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
        return await apiClient.get<OrderResponse[]>('/orders', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<OrderResponse>(`/orders/${id}`);
    },

    downloadPdf: async (id: number): Promise<string> => {
        const response = await apiClient.get(`/orders/${id}/pdf`, {
            responseType: 'blob'
        });
        return URL.createObjectURL(response.data);
    },

    create: async (request: OrderRequest) => {
        return await apiClient.post<OrderResponse>('/orders', request);
    },

    createWithBill: async (request: OrderWithBillRequest) => {
        return await apiClient.post<OrderResponse>('/orders/bills', request);
    },

    generatePdf: async (id: number) => {
        return await apiClient.post<{ message: string, path: string }>(`/orders/${id}/pdf`);
    },

    update: async (id: number, request: OrderUpdateRequest) => {
        return await apiClient.put(`/orders/${id}`, request);
    },

    patchStatus: async (id: number, request: OrderPatchRequest) => {
        return await apiClient.patch(`/orders/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/orders/${id}`);
    }
};

export const orderPriorityService = {
    getAll: async () => {
        return await apiClient.get<OrderPriorityResponse[]>('/order-priorities');
    }
};

export const orderStatusService = {
    getAll: async () => {
        return await apiClient.get<OrderStatusResponse[]>('/order-statuses');
    }
};