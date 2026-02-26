export interface SupplyRequest {
    supplierId: number;
    date: string;
}

export interface SupplyResponse {
    id: number;
    supplier: string;
    supplierId: number;
    date: string;
}

export interface SupplyFilter {
    supplierIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}