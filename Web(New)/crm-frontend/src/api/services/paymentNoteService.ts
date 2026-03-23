import { apiClient } from "../config.ts";
import type {
    PaymentMethodResponse,
    PaymentNoteFilter,
    PaymentNoteRequest,
    PaymentNoteResponse
} from "../types/paymentNote.ts";

export const paymentNoteService = {
    getPaged: async (filter: PaymentNoteFilter) => {
        return (await apiClient.get<PaymentNoteResponse[]>('/payment-notes', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<PaymentNoteResponse>(`/payment-notes/${id}`)).data;
    },

    create: async (request: PaymentNoteRequest) => {
        return (await apiClient.post<PaymentNoteResponse>('/payment-notes', request)).data;
    },

    update: async (id: number, method: number | null) => {
        return (await apiClient.put(`/payment-notes/${id}`, method)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/payment-notes/${id}`)).data;
    }
};

export const paymentMethodService = {
    getAll: async () => {
        return (await apiClient.get<PaymentMethodResponse[]>('/payment-methods')).data;
    }
};
