import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    SupplyFilter,
    SupplyRequest
} from "../api/types/supply.ts";
import { supplyService } from "../api/services/supplyService.ts";

export const supplyKeys = {
    all: ['supplies'] as const,
    lists: () => [...supplyKeys.all, 'list'] as const,
    list: (filter: SupplyFilter) => [...supplyKeys.lists(), filter] as const,
    details: () => [...supplyKeys.all, 'detail'] as const,
    detail: (id: number) => [...supplyKeys.details(), id] as const,
};

export const useSupplies = (filter: SupplyFilter) => {
    return useQuery({
        queryKey: supplyKeys.list(filter),
        queryFn: () => supplyService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useSupply = (id: number) => {
    return useQuery({
        queryKey: supplyKeys.detail(id),
        queryFn: () => supplyService.getById(id),
        enabled: !!id,
    });
};

export const useCreateSupply = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: SupplyRequest) => supplyService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: supplyKeys.lists() });
        },
    });
};

export const useDeleteSupply = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => supplyService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: supplyKeys.lists() });
        },
    });
};