import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    PartCategoryRequest,
    PartCategoryUpdateRequest
} from "../api/types/partCategory.ts";
import {partCategoryService} from "../api/services/partCategoryService.ts";


export const partCategoryKeys = {
    all: ['part-categories'] as const,
    lists: () => [...partCategoryKeys.all, 'list'] as const,
    details: () => [...partCategoryKeys.all, 'detail'] as const,
    detail: (id: number) => [...partCategoryKeys.details(), id] as const,
};

export const usePartCategories = () => {
    return useQuery({
        queryKey: partCategoryKeys.lists(),
        queryFn: () => partCategoryService.getAll(),
    });
};

export const usePartCategory = (id: number) => {
    return useQuery({
        queryKey: partCategoryKeys.detail(id),
        queryFn: () => partCategoryService.getById(id),
        enabled: !!id,
    });
};

export const useCreatePartCategory = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: PartCategoryRequest) => partCategoryService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partCategoryKeys.lists() });
        },
    });
};

export const useUpdatePartCategory = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: PartCategoryUpdateRequest }) =>
            partCategoryService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: partCategoryKeys.lists() });
            queryClient.invalidateQueries({ queryKey: partCategoryKeys.detail(variables.id) });
        },
    });
};

export const useDeletePartCategory = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => partCategoryService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: partCategoryKeys.lists() });
        },
    });
};