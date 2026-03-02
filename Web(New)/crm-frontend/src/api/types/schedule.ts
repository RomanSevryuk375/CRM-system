export interface ScheduleRequest {
    workerId: number;
    shiftId: number;
    dateTime: string;
}

export interface ScheduleResponse {
    id: number;
    worker: string;
    workerId: number;
    shift: string;
    shiftId: number;
    dateTime: string;
}

export interface ScheduleUpdateRequest {
    shiftId?: number;
    dateTime?: string;
}

export interface ScheduleWithShiftRequest {
    workerId: number;
    shiftId: number;
    dateTime: string;
    name: string;
    startAt: string;
    endAt: string;
}

export interface ScheduleFilter {
    workerIds?: number[];
    shiftIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}