export interface BillResponse {
    id: number;
    orderId: number;
    status: string;
    statusId: number;
    createdAt: string;
    amount: number;
    actualBillDate?: string;
}

export interface BillStatusResponse {
    id: number;
    name: string;
}

export interface BillRequest {
    orderId: number;
    statusId: number;
    createdAt: string;
    amount: number;
    actualBillDate?: string;
}

export interface BillUpdateRequest {
    statusId?: number;
    amount?: number;
    actualBillDate?: string;
}

export interface BillFilter {
    orderIds?: number[];
    clientIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}