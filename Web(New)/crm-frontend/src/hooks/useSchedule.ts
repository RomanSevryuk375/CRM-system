import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    ScheduleFilter,
    ScheduleRequest,
    ScheduleWithShiftRequest,
    ScheduleUpdateRequest
} from "../api/types/schedule.ts";
import { scheduleService } from "../api/services/scheduleService.ts";

export const scheduleKeys = {
    all: ['schedules'] as const,
    lists: () => [...scheduleKeys.all, 'list'] as const,
    list: (filter: ScheduleFilter) => [...scheduleKeys.lists(), filter] as const,
    details: () => [...scheduleKeys.all, 'detail'] as const,
    detail: (id: number) => [...scheduleKeys.details(), id] as const,
};

export const useSchedules = (filter: ScheduleFilter) => {
    return useQuery({
        queryKey: scheduleKeys.list(filter),
        queryFn: () => scheduleService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useSchedule = (id: number) => {
    return useQuery({
        queryKey: scheduleKeys.detail(id),
        queryFn: () => scheduleService.getById(id),
        enabled: !!id,
    });
};

export const useCreateSchedule = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ScheduleRequest) => scheduleService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: scheduleKeys.lists() });
        },
    });
};

export const useCreateScheduleWithShift = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ScheduleWithShiftRequest) => scheduleService.createWithShift(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: scheduleKeys.lists() });
        },
    });
};

export const useUpdateSchedule = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: ScheduleUpdateRequest }) =>
            scheduleService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: scheduleKeys.lists() });
            queryClient.invalidateQueries({ queryKey: scheduleKeys.detail(variables.id) });
        },
    });
};

export const useDeleteSchedule = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => scheduleService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: scheduleKeys.lists() });
        },
    });
};