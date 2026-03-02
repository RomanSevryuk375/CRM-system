export interface TaxRequest {
    name: string;
    rate: number;
    type: number; // TaxTypeEnum
}

export interface TaxResponse {
    id: number;
    name: string;
    rate: number;
    type: string;
}

export interface TaxUpdateRequest {
    name?: string;
    rate?: number;
}

export interface TaxTypeResponse {
    id: number;
    name: string;
}

export interface TaxFilter {
    taxTypeIds?: number[];
    sortBy?: string;
    isDescending: boolean;
}