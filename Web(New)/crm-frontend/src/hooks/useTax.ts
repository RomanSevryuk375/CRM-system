import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    TaxFilter,
    TaxRequest,
    TaxUpdateRequest
} from "../api/types/tax.ts";
import { taxService, taxTypeService } from "../api/services/taxService.ts";

export const taxKeys = {
    all: ['taxes'] as const,
    lists: () => [...taxKeys.all, 'list'] as const,
    list: (filter: TaxFilter) => [...taxKeys.lists(), filter] as const,
    details: () => [...taxKeys.all, 'detail'] as const,
    detail: (id: number) => [...taxKeys.details(), id] as const,
    types: ['tax-types'] as const,
};

export const useTaxes = (filter: TaxFilter) => {
    return useQuery({
        queryKey: taxKeys.list(filter),
        queryFn: () => taxService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useTax = (id: number) => {
    return useQuery({
        queryKey: taxKeys.detail(id),
        queryFn: () => taxService.getById(id),
        enabled: !!id,
    });
};

export const useTaxTypes = () => {
    return useQuery({
        queryKey: taxKeys.types,
        queryFn: () => taxTypeService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateTax = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: TaxRequest) => taxService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: taxKeys.lists() });
        },
    });
};

export const useUpdateTax = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: TaxUpdateRequest }) =>
            taxService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: taxKeys.lists() });
            queryClient.invalidateQueries({ queryKey: taxKeys.detail(variables.id) });
        },
    });
};

export const useDeleteTax = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => taxService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: taxKeys.lists() });
        },
    });
};