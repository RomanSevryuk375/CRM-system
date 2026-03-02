import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    OrderFilter,
    OrderRequest,
    OrderWithBillRequest,
    OrderUpdateRequest,
    OrderPatchRequest
} from "../api/types/order.ts";
import { orderService, orderPriorityService, orderStatusService } from "../api/services/orderService.ts";

export const orderKeys = {
    all: ['orders'] as const,
    lists: () => [...orderKeys.all, 'list'] as const,
    list: (filter: OrderFilter) => [...orderKeys.lists(), filter] as const,
    details: () => [...orderKeys.all, 'detail'] as const,
    detail: (id: number) => [...orderKeys.details(), id] as const,
    priorities: ['order-priorities'] as const,
    statuses: ['order-statuses'] as const,
    pdf: (id: number) => [...orderKeys.detail(id), 'pdf'] as const,
};

export const useOrders = (filter: OrderFilter) => {
    return useQuery({
        queryKey: orderKeys.list(filter),
        queryFn: () => orderService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useOrder = (id: number) => {
    return useQuery({
        queryKey: orderKeys.detail(id),
        queryFn: () => orderService.getById(id),
        enabled: !!id,
    });
};

export const useOrderPriorities = () => {
    return useQuery({
        queryKey: orderKeys.priorities,
        queryFn: () => orderPriorityService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useOrderStatuses = () => {
    return useQuery({
        queryKey: orderKeys.statuses,
        queryFn: () => orderStatusService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useOrderPdfFile = (id: number) => {
    return useQuery({
        queryKey: orderKeys.pdf(id),
        queryFn: () => orderService.downloadPdf(id),
        enabled: !!id,
        staleTime: Infinity,
    });
};

export const useCreateOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: OrderRequest) => orderService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: orderKeys.lists() });
        },
    });
};

export const useCreateOrderWithBill = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: OrderWithBillRequest) => orderService.createWithBill(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: orderKeys.lists() });
        },
    });
};

export const useUpdateOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: OrderUpdateRequest }) =>
            orderService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: orderKeys.lists() });
            queryClient.invalidateQueries({ queryKey: orderKeys.detail(variables.id) });
        },
    });
};

export const usePatchOrderStatus = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: OrderPatchRequest }) =>
            orderService.patchStatus(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: orderKeys.lists() });
            queryClient.invalidateQueries({ queryKey: orderKeys.detail(variables.id) });
        },
    });
};

export const useGenerateOrderPdf = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => orderService.generatePdf(id),
        onSuccess: (_, id) => {
            queryClient.invalidateQueries({ queryKey: orderKeys.detail(id) });
        },
    });
};

export const useDeleteOrder = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => orderService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: orderKeys.lists() });
        },
    });
};