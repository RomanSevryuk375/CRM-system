export interface PositionRequest {
    partId: number;
    cellId: number;
    purchasePrice: number;
    sellingPrice: number;
    quantity: number;
}

export interface PositionResponse {
    id: number;
    part: string;
    partId: number;
    cellId: number;
    purchasePrice: number;
    sellingPrice: number;
    quantity: number;
}

export interface PositionUpdateRequest {
    cellId?: number;
    purchasePrice?: number;
    sellingPrice?: number;
    quantity?: number;
}

export interface PositionWithPartRequest {
    partId: number;
    cellId: number;
    purchasePrice: number;
    sellingPrice: number;
    quantity: number;
    categoryId: number;
    oemArticle?: string;
    manufacturerArticle?: string;
    internalArticle: string;
    description?: string;
    name: string;
    manufacturer: string;
    applicability: string;
}

export interface PositionFilter {
    partIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}