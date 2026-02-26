export interface SkillRequest {
    workerId: number;
    specializationId: number;
}

export interface SkillResponse {
    id: number;
    workerId: number;
    worker: string;
    specializationId: number;
    specialization: string;
}

export interface SkillFilter {
    workerIds?: number[];
    specializationIds?: number[];
    sortBy?: string;
    isDescending: boolean;
}