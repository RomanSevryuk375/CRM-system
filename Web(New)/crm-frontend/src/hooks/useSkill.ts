import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { SkillFilter, SkillRequest } from "../api/types/skill.ts";
import { skillService } from "../api/services/skillService.ts";

export const skillKeys = {
    all: ['skills'] as const,
    lists: () => [...skillKeys.all, 'list'] as const,
    list: (filter: SkillFilter) => [...skillKeys.lists(), filter] as const,
    details: () => [...skillKeys.all, 'detail'] as const,
    detail: (id: number) => [...skillKeys.details(), id] as const,
};

export const useSkills = (filter: SkillFilter) => {
    return useQuery({
        queryKey: skillKeys.list(filter),
        queryFn: () => skillService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useSkill = (id: number) => {
    return useQuery({
        queryKey: skillKeys.detail(id),
        queryFn: () => skillService.getById(id),
        enabled: !!id,
    });
};

export const useCreateSkill = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: SkillRequest) => skillService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: skillKeys.lists() });
        },
    });
};

export const useDeleteSkill = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => skillService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: skillKeys.lists() });
        },
    });
};