export interface OrderPatchRequest {
    orderStatus: number; // OrderStatusEnum
}

export interface OrderRequest {
    statusId: number; // OrderStatusEnum
    carId: number;
    date: string;
    orderPdfFileName?: string;
    orderAgreementPdfFileName?: string;
    priorityId: number; // OrderPriorityEnum
}

export interface OrderResponse {
    id: number;
    status: string;
    statusId: number;
    car: string;
    carId: number;
    date: string;
    priority: string;
    priorityId: number;
}

export interface OrderWithBillRequest {
    orderStatusId: number; // OrderStatusEnum
    carId: number;
    date: string;
    priorityId: number; // OrderPriorityEnum
    ownerId: number;
    billStatusId: number; // BillStatusEnum
    createdAt: string;
    amount: number;
    actualBillDate?: string;
}

export interface OrderUpdateRequest {
    priorityId: number;
}

export interface OrderPriorityResponse {
    id: number;
    name: string;
}

export interface OrderStatusResponse {
    id: number;
    name: string;
}

export interface OrderFilter {
    orderIds?: number[];
    statusIds?: number[];
    priorityIds?: number[];
    carIds?: number[];
    clientIds?: number[];
    workerIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}