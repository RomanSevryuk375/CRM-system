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
        return (await apiClient.get<WorkInOrderResponse[]>('/works-in-order', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<WorkInOrderResponse>(`/works-in-order/${id}`)).data;
    },

    getByOrderId: async (orderId: number) => {
        return (await apiClient.get<WorkInOrderResponse[]>(`/works-in-order/orders/${orderId}`)).data;
    },

    create: async (request: WorkInOrderRequest) => {
        return (await apiClient.post<WorkInOrderResponse>('/works-in-order', request)).data;
    },

    update: async (id: number, request: WorkInOrderUpdateRequest) => {
        return (await apiClient.put(`/works-in-order/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/works-in-order/${id}`)).data;
    }
};

export const workInOrderStatusService = {
    getAll: async () => {
        return (await apiClient.get<WorkInOrderStatusResponse[]>('/work-in-order-statuses')).data;
    }
};
