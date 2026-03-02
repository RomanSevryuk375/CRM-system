export interface WorkRequest {
    title: string;
    category: string;
    description: string;
    standardTime: number;
}

export interface WorkResponse {
    id: number;
    title: string;
    category: string;
    description: string;
    standardTime: number;
}

export interface WorkUpdateRequest {
    title?: string;
    category?: string;
    description?: string;
    standardTime?: number;
}

export interface WorkFilter {
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}