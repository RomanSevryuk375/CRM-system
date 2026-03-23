import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    SupplierRequest,
    SupplierUpdateRequest
} from "../api/types/supplier.ts";
import { supplierService } from "../api/services/supplierService.ts";

export const supplierKeys = {
    all: ['suppliers'] as const,
    lists: () => [...supplierKeys.all, 'list'] as const,
    details: () => [...supplierKeys.all, 'detail'] as const,
    detail: (id: number) => [...supplierKeys.details(), id] as const,
};

export const useSuppliers = () => {
    return useQuery({
        queryKey: supplierKeys.lists(),
        queryFn: () => supplierService.getAll(),
    });
};

export const useSupplier = (id: number) => {
    return useQuery({
        queryKey: supplierKeys.detail(id),
        queryFn: () => supplierService.getById(id),
        enabled: !!id,
    });
};

export const useCreateSupplier = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: SupplierRequest) => supplierService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: supplierKeys.lists() });
        },
    });
};

export const useUpdateSupplier = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: SupplierUpdateRequest }) =>
            supplierService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: supplierKeys.lists() });
            queryClient.invalidateQueries({ queryKey: supplierKeys.detail(variables.id) });
        },
    });
};

export const useDeleteSupplier = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => supplierService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: supplierKeys.lists() });
        },
    });
};