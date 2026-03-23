import { apiClient } from "../config.ts";
import type {
    ScheduleFilter,
    ScheduleRequest,
    ScheduleResponse,
    ScheduleWithShiftRequest,
    ScheduleUpdateRequest
} from "../types/schedule.ts";

export const scheduleService = {
    getPaged: async (filter: ScheduleFilter) => {
        return (await apiClient.get<ScheduleResponse[]>('/schedules', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<ScheduleResponse>(`/schedules/${id}`)).data;
    },

    create: async (request: ScheduleRequest) => {
        return (await apiClient.post<ScheduleResponse>('/schedules', request)).data;
    },

    createWithShift: async (request: ScheduleWithShiftRequest) => {
        return (await apiClient.post<ScheduleResponse>('/schedules/with-shift', request)).data;
    },

    update: async (id: number, request: ScheduleUpdateRequest) => {
        return (await apiClient.put(`/schedules/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/schedules/${id}`)).data;
    }
};
