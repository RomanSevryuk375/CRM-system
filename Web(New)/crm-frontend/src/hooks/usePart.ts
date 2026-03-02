import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    PartFilter,
    PartRequest,
    PartUpdateRequest
} from "../api/types/part.ts";
import { partService } from "../api/services/partService.ts";

export const partKeys = {
    all: ['parts'] as const,
    lists: () => [...partKeys.all, 'list'] as const,
    list: (filter: PartFilter) => [...partKeys.lists(), filter] as const,
    details: () => [...partKeys.all, 'detail'] as const,
    detail: (id: number) => [...partKeys.details(), id] as const,
};

export const useParts = (filter: PartFilter) => {
    return useQuery({
        queryKey: partKeys.list(filter),
        queryFn: () => partService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const usePart = (id: number) => {
    return useQuery({
        queryKey: partKeys.detail(id),
        queryFn: () => partService.getById(id),
        enabled: !!id,
    });
};

export const useCreatePart = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: PartRequest) => partService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partKeys.lists() });
        },
    });
};

export const useUpdatePart = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: PartUpdateRequest }) =>
            partService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: partKeys.lists() });
            queryClient.invalidateQueries({ queryKey: partKeys.detail(variables.id) });
        },
    });
};

export const useDeletePart = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => partService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partKeys.lists() });
        },
    });
};