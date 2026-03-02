export interface SupplierRequest {
    name: string;
    contacts: string;
}

export interface SupplierResponse {
    id: number;
    name: string;
    contacts: string;
}

export interface SupplierUpdateRequest {
    name?: string;
    contacts?: string;
}