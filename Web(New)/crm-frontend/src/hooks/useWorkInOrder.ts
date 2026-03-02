import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    WorkInOrderFilter,
    WorkInOrderRequest,
    WorkInOrderUpdateRequest
} from "../api/types/workInOrder.ts";
import { workInOrderService, workInOrderStatusService } from "../api/services/workInOrderService.ts";

export const workInOrderKeys = {
    all: ['works-in-order'] as const,
    lists: () => [...workInOrderKeys.all, 'list'] as const,
    list: (filter: WorkInOrderFilter) => [...workInOrderKeys.lists(), filter] as const,
    details: () => [...workInOrderKeys.all, 'detail'] as const,
    detail: (id: number) => [...workInOrderKeys.details(), id] as const,
    byOrder: (orderId: number) => [...workInOrderKeys.lists(), 'by-order', orderId] as const,
    statuses: ['work-in-order-statuses'] as const,
};

export const useWorksInOrder = (filter: WorkInOrderFilter) => {
    return useQuery({
        queryKey: workInOrderKeys.list(filter),
        queryFn: () => workInOrderService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useWorkInOrder = (id: number) => {
    return useQuery({
        queryKey: workInOrderKeys.detail(id),
        queryFn: () => workInOrderService.getById(id),
        enabled: !!id,
    });
};

export const useWorksByOrderId = (orderId: number) => {
    return useQuery({
        queryKey: workInOrderKeys.byOrder(orderId),
        queryFn: () => workInOrderService.getByOrderId(orderId),
        enabled: !!orderId,
    });
};

export const useWorkInOrderStatuses = () => {
    return useQuery({
        queryKey: workInOrderKeys.statuses,
        queryFn: () => workInOrderStatusService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateWorkInOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: WorkInOrderRequest) => workInOrderService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workInOrderKeys.lists() });
        },
    });
};

export const useUpdateWorkInOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: WorkInOrderUpdateRequest }) =>
            workInOrderService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: workInOrderKeys.lists() });
            queryClient.invalidateQueries({ queryKey: workInOrderKeys.detail(variables.id) });
        },
    });
};

export const useDeleteWorkInOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => workInOrderService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workInOrderKeys.lists() });
        },
    });
};