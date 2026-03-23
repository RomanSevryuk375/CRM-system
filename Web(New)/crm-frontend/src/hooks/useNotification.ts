import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    NotificationFilter,
    NotificationRequest
} from "../api/types/notification.ts";
import {
    notificationService,
    notificationStatusService,
    notificationTypeService
} from "../api/services/notificationService.ts";

export const notificationKeys = {
    all: ['notifications'] as const,
    lists: () => [...notificationKeys.all, 'list'] as const,
    list: (filter: NotificationFilter) => [...notificationKeys.lists(), filter] as const,
    details: () => [...notificationKeys.all, 'detail'] as const,
    detail: (id: number) => [...notificationKeys.details(), id] as const,
    statuses: ['notification-statuses'] as const,
    types: ['notification-types'] as const,
};

export const useNotifications = (filter: NotificationFilter) => {
    return useQuery({
        queryKey: notificationKeys.list(filter),
        queryFn: () => notificationService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useNotification = (id: number) => {
    return useQuery({
        queryKey: notificationKeys.detail(id),
        queryFn: () => notificationService.getById(id),
        enabled: !!id,
    });
};

export const useNotificationStatuses = () => {
    return useQuery({
        queryKey: notificationKeys.statuses,
        queryFn: () => notificationStatusService.getAll(),
        staleTime: 1000 * 60 * 60, // 1 час
    });
};

export const useNotificationTypes = () => {
    return useQuery({
        queryKey: notificationKeys.types,
        queryFn: () => notificationTypeService.getAll(),
        staleTime: 1000 * 60 * 60, // 1 час
    });
};

export const useCreateNotification = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: NotificationRequest) => notificationService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: notificationKeys.lists() });
        },
    });
};

export const useDeleteNotification = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => notificationService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: notificationKeys.lists() });
        },
    });
};