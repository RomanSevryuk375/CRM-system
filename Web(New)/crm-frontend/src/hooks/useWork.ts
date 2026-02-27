import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    WorkFilter,
    WorkRequest,
    WorkUpdateRequest
} from "../api/types/work.ts";
import { workService } from "../api/services/workService.ts";

export const workKeys = {
    all: ['works'] as const,
    lists: () => [...workKeys.all, 'list'] as const,
    list: (filter: WorkFilter) => [...workKeys.lists(), filter] as const,
    details: () => [...workKeys.all, 'detail'] as const,
    detail: (id: number) => [...workKeys.details(), id] as const,
};

export const useWorks = (filter: WorkFilter) => {
    return useQuery({
        queryKey: workKeys.list(filter),
        queryFn: () => workService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useWork = (id: number) => {
    return useQuery({
        queryKey: workKeys.detail(id),
        queryFn: () => workService.getById(id),
        enabled: !!id,
    });
};

export const useCreateWork = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: WorkRequest) => workService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workKeys.lists() });
        },
    });
};

export const useUpdateWork = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: WorkUpdateRequest }) =>
            workService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: workKeys.lists() });
            queryClient.invalidateQueries({ queryKey: workKeys.detail(variables.id) });
        },
    });
};

export const useDeleteWork = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => workService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workKeys.lists() });
        },
    });
};