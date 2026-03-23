import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    GuaranteeFilter,
    GuaranteeRequest,
    GuaranteeUpdateRequest
} from "../api/types/guarantee.ts";
import { guaranteeService } from "../api/services/guaranteeService.ts";

export const guaranteeKeys = {
    all: ['guarantees'] as const,
    lists: () => [...guaranteeKeys.all, 'list'] as const,
    list: (filter: GuaranteeFilter) => [...guaranteeKeys.lists(), filter] as const,
    details: () => [...guaranteeKeys.all, 'detail'] as const,
    detail: (id: number) => [...guaranteeKeys.details(), id] as const,
};

export const useGuarantees = (filter: GuaranteeFilter) => {
    return useQuery({
        queryKey: guaranteeKeys.list(filter),
        queryFn: () => guaranteeService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useGuarantee = (id: number) => {
    return useQuery({
        queryKey: guaranteeKeys.detail(id),
        queryFn: () => guaranteeService.getById(id),
        enabled: !!id,
    });
};

export const useCreateGuarantee = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: GuaranteeRequest) => guaranteeService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: guaranteeKeys.lists() });
        },
    });
};

export const useUpdateGuarantee = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: GuaranteeUpdateRequest }) =>
            guaranteeService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: guaranteeKeys.lists() });
            queryClient.invalidateQueries({ queryKey: guaranteeKeys.detail(variables.id) });
        },
    });
};

export const useDeleteGuarantee = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => guaranteeService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: guaranteeKeys.lists() });
        },
    });
};