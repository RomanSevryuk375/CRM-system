import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    ShiftRequest,
    ShiftUpdateRequest
} from "../api/types/shift.ts";
import { shiftService } from "../api/services/shiftService.ts";

export const shiftKeys = {
    all: ['shifts'] as const,
    lists: () => [...shiftKeys.all, 'list'] as const,
    details: () => [...shiftKeys.all, 'detail'] as const,
    detail: (id: number) => [...shiftKeys.details(), id] as const,
};

export const useShifts = () => {
    return useQuery({
        queryKey: shiftKeys.lists(),
        queryFn: () => shiftService.getAll(),
    });
};

export const useShift = (id: number) => {
    return useQuery({
        queryKey: shiftKeys.detail(id),
        queryFn: () => shiftService.getById(id),
        enabled: !!id,
    });
};

export const useCreateShift = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ShiftRequest) => shiftService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: shiftKeys.lists() });
        },
    });
};

export const useUpdateShift = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: ShiftUpdateRequest }) =>
            shiftService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: shiftKeys.lists() });
            queryClient.invalidateQueries({ queryKey: shiftKeys.detail(variables.id) });
        },
    });
};

export const useDeleteShift = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => shiftService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: shiftKeys.lists() });
        },
    });
};