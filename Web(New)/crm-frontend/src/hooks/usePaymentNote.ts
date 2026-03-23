import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    PaymentNoteFilter,
    PaymentNoteRequest
} from "../api/types/paymentNote.ts";
import { paymentNoteService, paymentMethodService } from "../api/services/paymentNoteService.ts";

export const paymentNoteKeys = {
    all: ['payment-notes'] as const,
    lists: () => [...paymentNoteKeys.all, 'list'] as const,
    list: (filter: PaymentNoteFilter) => [...paymentNoteKeys.lists(), filter] as const,
    details: () => [...paymentNoteKeys.all, 'detail'] as const,
    detail: (id: number) => [...paymentNoteKeys.details(), id] as const,
    methods: ['payment-methods'] as const,
};

export const usePaymentNotes = (filter: PaymentNoteFilter) => {
    return useQuery({
        queryKey: paymentNoteKeys.list(filter),
        queryFn: () => paymentNoteService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const usePaymentNote = (id: number) => {
    return useQuery({
        queryKey: paymentNoteKeys.detail(id),
        queryFn: () => paymentNoteService.getById(id),
        enabled: !!id,
    });
};

export const usePaymentMethods = () => {
    return useQuery({
        queryKey: paymentNoteKeys.methods,
        queryFn: () => paymentMethodService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreatePaymentNote = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: PaymentNoteRequest) => paymentNoteService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: paymentNoteKeys.lists() });
        },
    });
};

export const useUpdatePaymentNote = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, method }: { id: number; method: number | null }) =>
            paymentNoteService.update(id, method),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: paymentNoteKeys.lists() });
            queryClient.invalidateQueries({ queryKey: paymentNoteKeys.detail(variables.id) });
        },
    });
};

export const useDeletePaymentNote = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => paymentNoteService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: paymentNoteKeys.lists() });
        },
    });
};