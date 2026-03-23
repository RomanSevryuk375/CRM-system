import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    PositionFilter,
    PositionWithPartRequest,
    PositionUpdateRequest
} from "../api/types/position.ts";
import { positionService } from "../api/services/positionService.ts";

export const positionKeys = {
    all: ['positions'] as const,
    lists: () => [...positionKeys.all, 'list'] as const,
    list: (filter: PositionFilter) => [...positionKeys.lists(), filter] as const,
    details: () => [...positionKeys.all, 'detail'] as const,
    detail: (id: number) => [...positionKeys.details(), id] as const,
};

export const usePositions = (filter: PositionFilter) => {
    return useQuery({
        queryKey: positionKeys.list(filter),
        queryFn: () => positionService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const usePosition = (id: number) => {
    return useQuery({
        queryKey: positionKeys.detail(id),
        queryFn: () => positionService.getById(id),
        enabled: !!id,
    });
};

export const useCreatePositionWithPart = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: PositionWithPartRequest) => positionService.createWithPart(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: positionKeys.lists() });
        },
    });
};

export const useUpdatePosition = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: PositionUpdateRequest }) =>
            positionService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: positionKeys.lists() });
            queryClient.invalidateQueries({ queryKey: positionKeys.detail(variables.id) });
        },
    });
};

export const useDeletePosition = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => positionService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: positionKeys.lists() });
        },
    });
};