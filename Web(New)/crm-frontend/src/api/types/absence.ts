export interface AbsenceResponse {
    id: number;
    workerName: string;
    workerId: number;
    typeName: string;
    typeId: number;
    startDate: string;
    endDate: string;
}

export interface AbsenceTypeResponse {
    id: number;
    name: string;
}

export interface AbsenceRequest {
    workerId: number;
    typeId: number;
    startDate: string;
    endDate: string;
}

export interface AbsenceUpdateRequest {
    typeId?: number;
    startDate?: string;
    endDate?: string;
}

export interface AbsenceFilter {
    workerIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}