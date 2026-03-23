import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import type {
    AcceptanceFilter,
    AcceptanceImgFilter,
    AcceptanceRequest,
    AcceptanceUpdateRequest
} from "../api/types/acceptance.ts";
import {acceptanceImageService, acceptanceService} from "../api/services/acceptanceService.ts";

export const acceptanceKeys = {
    all: ['acceptances'] as const,
    lists: () => [...acceptanceKeys.all, 'list'] as const,
    list: (filter: AcceptanceFilter) => [...acceptanceKeys.lists(), filter] as const,
    details: () => [...acceptanceKeys.all, 'detail'] as const,
    detail: (id: number) => [...acceptanceKeys.details(), id] as const,

    imagesAll: ['acceptance-images'] as const,
    imagesList: (filter: AcceptanceImgFilter) => [...acceptanceKeys.imagesAll, 'list', filter] as const,
    imagesByAcceptance: (acceptanceId: number) => [...acceptanceKeys.imagesAll, 'by-acceptance', acceptanceId] as const,
};

export const useAcceptances = (filter: AcceptanceFilter) => {
    return useQuery({
        queryKey: acceptanceKeys.list(filter),
        queryFn: () => acceptanceService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useAcceptance = (id: number) => {
    return useQuery({
        queryKey: acceptanceKeys.detail(id),
        queryFn: () => acceptanceService.getById(id),
        enabled: !!id,
    });
};

export const useCreateAcceptance = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: AcceptanceRequest) => acceptanceService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: acceptanceKeys.lists() });
        },
    });
};

export const useUpdateAcceptance = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: AcceptanceUpdateRequest }) =>
            acceptanceService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: acceptanceKeys.lists() });
            queryClient.invalidateQueries({ queryKey: acceptanceKeys.detail(variables.id) });
        },
    });
};

export const useDeleteAcceptance = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => acceptanceService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: acceptanceKeys.lists() });
        },
    });
};

export const useAcceptanceImages = (filter: AcceptanceImgFilter) => {
    return useQuery({
        queryKey: acceptanceKeys.imagesList(filter),
        queryFn: () => acceptanceImageService.getPaged(filter),
    });
};

export const useUploadAcceptanceImage = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ acceptanceId, file, description }: { acceptanceId: number; file: File; description?: string }) =>
            acceptanceImageService.upload(acceptanceId, file, description),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: [...acceptanceKeys.imagesAll, 'list']
            });
        },
    });
};

export const useDeleteAcceptanceImage = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => acceptanceImageService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: acceptanceKeys.imagesAll });
        },
    });
};

export const useAcceptanceImageFile = (id: number) => {
    return useQuery({
        queryKey: [...acceptanceKeys.imagesAll, 'file', id],
        queryFn: () => acceptanceImageService.downloadFile(id),
        staleTime: Infinity,
    });
};
