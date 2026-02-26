export interface ClientResponse {
    id: number;
    userId: number;
    name: string;
    surname: string;
    phoneNumber: string;
    email: string;
}

export interface ClientRequest {
    userId: number;
    name: string;
    surname: string;
    phoneNumber: string;
    email: string;
}

export interface ClientRegisterRequest {
    userId: number;
    name: string;
    surname: string;
    phoneNumber: string;
    email: string;
    roleId: number;
    login: string;
    password: string;
}

export interface ClientUpdateRequest {
    name?: string;
    surname?: string;
    phoneNumber?: string;
    email?: string;
}

export interface ClientFilter {
    clientIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}