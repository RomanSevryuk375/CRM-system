export interface WorkInOrderRequest {
    orderId: number;
    jobId: number;
    workerId: number;
    statusId: number; // WorkStatusEnum
    timeSpent: number;
}

export interface WorkInOrderResponse {
    id: number;
    orderId: number;
    job: string;
    jobId: number;
    worker: string;
    workerId: number;
    status: string;
    statusId: number;
    timeSpent: number;
}

export interface WorkInOrderUpdateRequest {
    workerId?: number;
    statusId?: number; // WorkStatusEnum
    timeSpent?: number;
}

export interface WorkInOrderStatusResponse {
    id: number;
    name: string;
}

export interface WorkInOrderFilter {
    orderIds?: number[];
    jobIds?: number[];
    workerIds?: number[];
    statusIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}