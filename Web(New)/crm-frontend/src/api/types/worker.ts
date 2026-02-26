export interface WorkerRequest {
    userId: number;
    name: string;
    surname: string;
    hourlyRate: number;
    phoneNumber: string;
    email: string;
}

export interface WorkerResponse {
    id: number;
    userId: number;
    name: string;
    surname: string;
    hourlyRate: number;
    phoneNumber: string;
    email: string;
}

export interface WorkerUpdateRequest {
    userId?: number;
    name?: string;
    surname?: string;
    hourlyRate?: number;
    phoneNumber?: string;
    email?: string;
}

export interface WorkerWithUserRequest {
    name: string;
    surname: string;
    hourlyRate: number;
    phoneNumber: string;
    email: string;
    roleId: number;
    login: string;
    password: string;
}

export interface WorkerFilter {
    workerIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}