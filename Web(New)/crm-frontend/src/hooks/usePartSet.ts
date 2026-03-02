import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    PartSetFilter,
    PartSetRequest,
    PartSetUpdateRequest
} from "../api/types/partSet.ts";
import { partSetService } from "../api/services/partSetService.ts";

export const partSetKeys = {
    all: ['part-sets'] as const,
    lists: () => [...partSetKeys.all, 'list'] as const,
    list: (filter: PartSetFilter) => [...partSetKeys.lists(), filter] as const,
    details: () => [...partSetKeys.all, 'detail'] as const,
    detail: (id: number) => [...partSetKeys.details(), id] as const,
    byOrder: (orderId: number) => [...partSetKeys.lists(), 'by-order', orderId] as const,
};

export const usePartSets = (filter: PartSetFilter) => {
    return useQuery({
        queryKey: partSetKeys.list(filter),
        queryFn: () => partSetService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const usePartSet = (id: number) => {
    return useQuery({
        queryKey: partSetKeys.detail(id),
        queryFn: () => partSetService.getById(id),
        enabled: !!id,
    });
};

export const usePartSetsByOrder = (orderId: number) => {
    return useQuery({
        queryKey: partSetKeys.byOrder(orderId),
        queryFn: () => partSetService.getByOrderId(orderId),
        enabled: !!orderId,
    });
};

export const useCreatePartSet = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: PartSetRequest) => partSetService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partSetKeys.lists() });
        },
    });
};

export const useUpdatePartSet = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: PartSetUpdateRequest }) =>
            partSetService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: partSetKeys.lists() });
            queryClient.invalidateQueries({ queryKey: partSetKeys.detail(variables.id) });
        },
    });
};

export const useDeletePartSet = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => partSetService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partSetKeys.lists() });
        },
    });
};