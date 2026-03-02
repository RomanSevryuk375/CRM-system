export interface SupplySetRequest {
    supplyId: number;
    positionId: number;
    quantity: number;
    purchasePrice: number;
}

export interface SupplySetResponse {
    id: number;
    supplyId: number;
    position: string;
    positionId: number;
    quantity: number;
    purchasePrice: number;
}

export interface SupplySetUpdateRequest {
    quantity?: number;
    purchasePrice?: number;
}

export interface SupplySetFilter {
    supplyIds?: number[];
    positionIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}