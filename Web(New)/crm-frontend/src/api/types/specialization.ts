export interface SpecializationRequest {
    name: string;
}

export interface SpecializationResponse {
    id: number;
    name: string;
}

export interface SpecializationUpdateRequest {
    name?: string;
}