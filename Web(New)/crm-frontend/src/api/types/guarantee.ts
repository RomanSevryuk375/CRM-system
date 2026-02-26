export interface GuaranteeResponse {
    id: number;
    orderId: number;
    dateStart: string;
    dateEnd: string;
    description?: string;
    terms: string;
}

export interface GuaranteeRequest {
    orderId: number;
    dateStart: string;
    dateEnd: string;
    description?: string;
    terms: string;
}

export interface GuaranteeUpdateRequest {
    description?: string;
    terms?: string;
}

export interface GuaranteeFilter {
    orderIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}