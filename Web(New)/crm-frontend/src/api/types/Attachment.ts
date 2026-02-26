export interface AttachmentResponse {
    id: number;
    orderId: number;
    worker: string;
    workerId: number;
    createdAt: string;
    description?: string;
}

export interface AttachmentImgResponse {
    id: number;
    attachmentId: number;
    filePath: string;
    description?: string;
}

export interface AttachmentRequest {
    orderId: number;
    workerId: number;
    createdAt: string;
    description?: string;
}

export interface AttachmentUpdateRequest {
    description?: string;
}

export interface AttachmentImgUpdateRequest {
    filePath?: string;
    description?: string;
}

export interface AttachmentFilter {
    attachmentIds?: number[];
    workerIds?: number[];
    orderIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}

export interface AttachmentFilter {
    attachmentIds?: number[];
    page: number;
    limit: number;
}