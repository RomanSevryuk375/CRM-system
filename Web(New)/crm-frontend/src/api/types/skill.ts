export interface SkillRequest {
    workerId: number;
    specializationId: number;
}

export interface SkillResponse {
    id: number;
    workerId: number;
    specializationId: number;
}

export interface SkillFilter {
    workerIds?: number[];
    specializationIds?: number[];
    sortBy?: string;
    isDescending: boolean;
}