export interface AcceptanceResponse {
    id: number;
    orderId: number;
    worker: string;
    workerId: number;
    createdAt: string;
    mileage: number;
    flueLevel: number;
    externalDefects?: string;
    internalDefects?: string;
    clientSign?: boolean;
    workerSign?: boolean;
}

export interface AcceptanceImgResponse {
    id: number;
    acceptanceId: number;
    filePath: string;
    description?: string;
}

export interface AcceptanceRequest {
    orderId: number;
    workerId: number;
    createdAt: string;
    mileage: number;
    flueLevel: number;
    externalDefects?: string;
    internalDefects?: string;
    clientSign?: boolean;
    workerSign?: boolean;
}

export interface AcceptanceUpdateRequest {
    mileage?: number;
    flueLevel?: number;
    externalDefects?: string;
    internalDefects?: string;
    clientSign?: boolean;
    workerSign?: boolean;
}

export interface AcceptanceImgUpdateRequest {
    filePath?: string;
    description?: string;
}

export interface AcceptanceFilter {
    acceptanceIds?: number[];
    workerIds?: number[];
    orderIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}

export interface AcceptanceImgFilter {
    acceptanceIds?: number[];
    page: number;
    limit: number;
}