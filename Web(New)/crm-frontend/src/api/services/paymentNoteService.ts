import { apiClient } from "../config.ts";
import type {
    PaymentMethodResponse,
    PaymentNoteFilter,
    PaymentNoteRequest,
    PaymentNoteResponse
} from "../types/paymentNote.ts";

export const paymentNoteService = {
    getPaged: async (filter: PaymentNoteFilter) => {
        return await apiClient.get<PaymentNoteResponse[]>('/payment-notes', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<PaymentNoteResponse>(`/payment-notes/${id}`);
    },

    create: async (request: PaymentNoteRequest) => {
        return await apiClient.post<PaymentNoteResponse>('/payment-notes', request);
    },

    update: async (id: number, method: number | null) => {
        return await apiClient.put(`/payment-notes/${id}`, method);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/payment-notes/${id}`);
    }
};

export const paymentMethodService = {
    getAll: async () => {
        return await apiClient.get<PaymentMethodResponse[]>('/payment-methods');
    }
};