export interface ShiftRequest {
    name: string;
    startAt: string;
    endAt: string;
}

export interface ShiftResponse {
    id: number;
    name: string;
    startAt: string;
    endAt: string;
}

export interface ShiftUpdateRequest {
    name?: string;
    startAt?: string;
    endAt?: string;
}