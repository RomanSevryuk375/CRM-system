export interface PartCategoryRequest {
    name: string;
    description?: string;
}

export interface PartCategoryResponse {
    id: number;
    name: string;
    description?: string;
}

export interface PartCategoryUpdateRequest {
    name?: string;
    description?: string;
}