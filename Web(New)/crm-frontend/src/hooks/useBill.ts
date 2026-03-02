import type {BillFilter, BillRequest, BillUpdateRequest} from "../api/types/bill.ts";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {billService, billStatusService} from "../api/services/billService.ts";

export const billKeys = {
    all: ['bills'] as const,
    lists: () => [...billKeys.all, 'list'] as const,
    list: (filter: BillFilter) => [...billKeys.lists(), filter] as const,
    details: () => [...billKeys.all, 'detail'] as const,
    detail: (id: number) => [...billKeys.details(), id] as const,
    statuses:['bill-statuses'] as const,
    debt: (id: number) => [...billKeys.all, 'debt', id] as const,
};

export const useBills = (filter: BillFilter) => {
    return useQuery({
        queryKey: billKeys.list(filter),
        queryFn: () => billService.getPaged(filter),
        placeholderData: (previousValue) => previousValue,
    });
};

export const useBill = (id: number) => {
    return useQuery({
        queryKey: billKeys.detail(id),
        queryFn: () => billService.getById(id),
        enabled: !!id,
    });
};

export const useBillStatuses = () => {
    return useQuery({
        queryKey: billKeys.statuses,
        queryFn: () => billStatusService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useBillDebt = (id: number) => {
    return useQuery({
        queryKey: billKeys.debt(id),
        queryFn: () => billService.fetchDebt(id),
        enabled: !!id,
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateBill = async () => {
    const  queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: BillRequest) => billService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: billKeys.lists() });
        },
    });
};

export const useUpdateBill = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: BillUpdateRequest }) =>
            billService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: billKeys.lists() });
            queryClient.invalidateQueries({ queryKey: billKeys.debt(variables.id) });
            queryClient.invalidateQueries({ queryKey: billKeys.detail(variables.id) });
        },
    });
};

export const useDeleteBill = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => billService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: billKeys.lists() });
        },
    });
};
