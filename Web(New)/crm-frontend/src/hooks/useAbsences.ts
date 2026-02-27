import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import type {AbsenceFilter, AbsenceRequest, AbsenceUpdateRequest} from "../api/types/absence.ts";
import {absenceService, absenceTypeService} from "../api/services/absenceService.ts";

export const absenceKeys = {
    all: ['absences'] as const,
    lists: () => [...absenceKeys.all, 'list'] as const,
    list: (filter: AbsenceFilter) => [...absenceKeys.lists(), filter] as const,
    details: () => [...absenceKeys.all, 'detail'] as const,
    detail: (id: number) => [...absenceKeys.details(), id] as const,
    types: ['absence-types'] as const,
};

export const useAbsences = (filter: AbsenceFilter) => {
    return useQuery({
        queryKey: absenceKeys.list(filter),
        queryFn: () => absenceService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useAbsence = (id: number) => {
    return useQuery({
        queryKey: absenceKeys.detail(id),
        queryFn: () => absenceService.getById(id),
        enabled: !!id,
    });
};

export const useAbsenceTypes = () => {
    return useQuery({
        queryKey: absenceKeys.types,
        queryFn: () => absenceTypeService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateAbsence = async () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (data: AbsenceRequest) => absenceService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: absenceKeys.lists() });
        },
    });
};

export const useUpdateAbsence = async () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: AbsenceUpdateRequest }) =>
            absenceService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: absenceKeys.lists() });
            queryClient.invalidateQueries({ queryKey: absenceKeys.detail(variables.id) });
        },
    });
};

export const useDeleteAbsence = async () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (id: number) => absenceService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: absenceKeys.lists() });
        },
    });
};