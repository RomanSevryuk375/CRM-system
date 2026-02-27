import { apiClient } from "../config.ts";
import type {
    NotificationFilter,
    NotificationRequest,
    NotificationResponse,
    NotificationStatusResponse,
    NotificationTypeResponse
} from "../types/notification.ts";

export const notificationService = {
    getPaged: async (filter: NotificationFilter) => {
        return await apiClient.get<NotificationResponse[]>('/notifications', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<NotificationResponse>(`/notifications/${id}`);
    },

    create: async (request: NotificationRequest) => {
        return await apiClient.post<NotificationResponse>('/notifications', request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/notifications/${id}`);
    }
};

export const notificationStatusService = {
    getAll: async () => {
        return await apiClient.get<NotificationStatusResponse[]>('/notification-statuses');
    }
};

export const notificationTypeService = {
    getAll: async () => {
        return await apiClient.get<NotificationTypeResponse[]>('/notification-types');
    }
};