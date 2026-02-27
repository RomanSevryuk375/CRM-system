import { apiClient } from "../config.ts";
import type {
    WorkInOrderFilter,
    WorkInOrderRequest,
    WorkInOrderResponse,
    WorkInOrderUpdateRequest,
    WorkInOrderStatusResponse
} from "../types/workInOrder.ts";

export const workInOrderService = {
    getPaged: async (filter: WorkInOrderFilter) => {
        return await apiClient.get<WorkInOrderResponse[]>('/works-in-order', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<WorkInOrderResponse>(`/works-in-order/${id}`);
    },

    getByOrderId: async (orderId: number) => {
        return await apiClient.get<WorkInOrderResponse[]>(`/works-in-order/orders/${orderId}`);
    },

    create: async (request: WorkInOrderRequest) => {
        return await apiClient.post<WorkInOrderResponse>('/works-in-order', request);
    },

    update: async (id: number, request: WorkInOrderUpdateRequest) => {
        return await apiClient.put(`/works-in-order/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/works-in-order/${id}`);
    }
};

export const workInOrderStatusService = {
    getAll: async () => {
        return await apiClient.get<WorkInOrderStatusResponse[]>('/work-in-order-statuses');
    }
};