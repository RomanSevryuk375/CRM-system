import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    SpecializationRequest,
    SpecializationUpdateRequest
} from "../api/types/specialization.ts";
import { specializationService } from "../api/services/specializationService.ts";

export const specializationKeys = {
    all: ['specializations'] as const,
    lists: () => [...specializationKeys.all, 'list'] as const,
    details: () => [...specializationKeys.all, 'detail'] as const,
    detail: (id: number) => [...specializationKeys.details(), id] as const,
};

export const useSpecializations = () => {
    return useQuery({
        queryKey: specializationKeys.lists(),
        queryFn: () => specializationService.getAll(),
    });
};

export const useSpecialization = (id: number) => {
    return useQuery({
        queryKey: specializationKeys.detail(id),
        queryFn: () => specializationService.getById(id),
        enabled: !!id,
    });
};

export const useCreateSpecialization = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: SpecializationRequest) => specializationService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: specializationKeys.lists() });
        },
    });
};

export const useUpdateSpecialization = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: SpecializationUpdateRequest }) =>
            specializationService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: specializationKeys.lists() });
            queryClient.invalidateQueries({ queryKey: specializationKeys.detail(variables.id) });
        },
    });
};

export const useDeleteSpecialization = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => specializationService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: specializationKeys.lists() });
        },
    });
};