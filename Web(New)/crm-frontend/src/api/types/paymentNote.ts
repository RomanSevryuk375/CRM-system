export interface PaymentNoteRequest {
    billId: number;
    date: string;
    amount: number;
    methodId: number; // PaymentMethodEnum
}

export interface PaymentNoteResponse {
    id: number;
    billId: number;
    date: string;
    amount: number;
    methodId: number; // PaymentMethodEnum
}

export interface PaymentMethodResponse {
    id: number;
    name: string;
}

export interface PaymentNoteFilter {
    billIds?: number[];
    methodIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}