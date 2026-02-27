import { apiClient } from "../config.ts";
import type {
    ExpenseFilter,
    ExpenseRequest,
    ExpenseResponse,
    ExpenseTypeResponse,
    ExpenseUpdateRequest
} from "../types/expense.ts";

export const expenseService = {
    getPaged: async (filter: ExpenseFilter) => {
        return await apiClient.get<ExpenseResponse[]>('/expenses', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<ExpenseResponse>(`/expenses/${id}`);
    },

    create: async (request: ExpenseRequest) => {
        return await apiClient.post<ExpenseResponse>('/expenses', request);
    },

    update: async (id: number, request: ExpenseUpdateRequest) => {
        return await apiClient.put(`/expenses/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/expenses/${id}`);
    }
};

export const expenseTypeService = {
    getAll: async () => {
        return await apiClient.get<ExpenseTypeResponse[]>('/expense-types');
    }
};