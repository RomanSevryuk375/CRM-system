export interface ProposalStatusRequest {
    status: number; // ProposalStatusEnum
}

export interface WorkProposalRequest {
    orderId: number;
    jobId: number;
    workerId: number;
    statusId: number; // ProposalStatusEnum
    date: string;
}

export interface WorkProposalResponse {
    id: number;
    orderId: number;
    job: string;
    jobId: number;
    worker: string;
    workerId: number;
    status: string;
    statusId: number;
    date: string;
}

export interface WorkProposalStatusResponse {
    id: number;
    name: string;
}

export interface WorkProposalFilter {
    orderIds?: number[];
    jobIds?: number[];
    workerIds?: number[];
    statusIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}