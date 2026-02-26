export interface PartSetFilter {
    orderIds?: number[];
    positionIds?: number[];
    proposalIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}

export interface PartSetRequest {
    orderId?: number;
    positionId: number;
    proposalId?: number;
    quantity: number;
    soldPrice: number;
}

export interface PartSetResponse {
    id: number;
    orderId?: number;
    position: string;
    positionId: number;
    proposalId?: number;
    quantity: number;
    soldPrice: number;
}

export interface PartSetUpdateRequest {
    quantity?: number;
    soldPrice?: number;
}