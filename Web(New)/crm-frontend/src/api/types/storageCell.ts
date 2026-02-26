export interface StorageCellRequest {
    rack: string;
    shelf: string;
}

export interface StorageCellResponse {
    id: number;
    rack: string;
    shelf: string;
}

export interface StorageCellUpdateRequest {
    rack?: string;
    shelf?: string;
}