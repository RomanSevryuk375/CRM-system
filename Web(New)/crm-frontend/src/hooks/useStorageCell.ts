import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    StorageCellRequest,
    StorageCellUpdateRequest
} from "../api/types/storageCell.ts";
import { storageCellService } from "../api/services/storageCellService.ts";

export const storageCellKeys = {
    all: ['storage-cells'] as const,
    lists: () => [...storageCellKeys.all, 'list'] as const,
    details: () => [...storageCellKeys.all, 'detail'] as const,
    detail: (id: number) => [...storageCellKeys.details(), id] as const,
};

export const useStorageCells = () => {
    return useQuery({
        queryKey: storageCellKeys.lists(),
        queryFn: () => storageCellService.getAll(),
    });
};

export const useStorageCell = (id: number) => {
    return useQuery({
        queryKey: storageCellKeys.detail(id),
        queryFn: () => storageCellService.getById(id),
        enabled: !!id,
    });
};

export const useCreateStorageCell = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: StorageCellRequest) => storageCellService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: storageCellKeys.lists() });
        },
    });
};

export const useUpdateStorageCell = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: StorageCellUpdateRequest }) =>
            storageCellService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: storageCellKeys.lists() });
            queryClient.invalidateQueries({ queryKey: storageCellKeys.detail(variables.id) });
        },
    });
};

export const useDeleteStorageCell = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => storageCellService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: storageCellKeys.lists() });
        },
    });
};