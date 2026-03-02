import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    ExpenseFilter,
    ExpenseRequest,
    ExpenseUpdateRequest
} from "../api/types/expense.ts";
import { expenseService, expenseTypeService } from "../api/services/expenseService.ts";

export const expenseKeys = {
    all: ['expenses'] as const,
    lists: () => [...expenseKeys.all, 'list'] as const,
    list: (filter: ExpenseFilter) => [...expenseKeys.lists(), filter] as const,
    details: () => [...expenseKeys.all, 'detail'] as const,
    detail: (id: number) => [...expenseKeys.details(), id] as const,
    types: ['expense-types'] as const,
};

export const useExpenses = (filter: ExpenseFilter) => {
    return useQuery({
        queryKey: expenseKeys.list(filter),
        queryFn: () => expenseService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useExpense = (id: number) => {
    return useQuery({
        queryKey: expenseKeys.detail(id),
        queryFn: () => expenseService.getById(id),
        enabled: !!id,
    });
};

export const useExpenseTypes = () => {
    return useQuery({
        queryKey: expenseKeys.types,
        queryFn: () => expenseTypeService.getAll(),
        staleTime: 1000 * 60 * 60, // 1 час
    });
};

export const useCreateExpense = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ExpenseRequest) => expenseService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: expenseKeys.lists() });
        },
    });
};

export const useUpdateExpense = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: ExpenseUpdateRequest }) =>
            expenseService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: expenseKeys.lists() });
            queryClient.invalidateQueries({ queryKey: expenseKeys.detail(variables.id) });
        },
    });
};

export const useDeleteExpense = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => expenseService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: expenseKeys.lists() });
        },
    });
};