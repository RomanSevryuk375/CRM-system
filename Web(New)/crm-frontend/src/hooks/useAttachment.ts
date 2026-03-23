import type {
    AttachmentFilter,
    AttachmentImgFilter,
    AttachmentRequest,
    AttachmentUpdateRequest
} from "../api/types/attachment.ts";
import {useMutation, useQuery, useQueryClient} from "@tanstack/react-query";
import {attachmentImageService, attachmentService} from "../api/services/attachmentService.ts";

export const attachmentKeys = {
    all: ['attachments'] as const,
    lists: () => [...attachmentKeys.all, 'list'] as const,
    list: (filter: AttachmentFilter) => [...attachmentKeys.lists(), filter] as const,
    details: () => [...attachmentKeys.all, 'detail'] as const,
    detail: (id: number) => [...attachmentKeys.details(), id] as const,

    imagesAll: ['attachment-images'] as const,
    imagesList: (filter: AttachmentImgFilter) => [...attachmentKeys.imagesAll, 'list', filter] as const,
    imagesByAttachment: (attachmentId: number) => [...attachmentKeys.imagesAll, 'by-attachment', attachmentId] as const,
};

export const useAttachments = (filter: AttachmentFilter) => {
    return useQuery({
        queryKey: attachmentKeys.list(filter),
        queryFn: () => attachmentImageService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    })
};

export const useAttachment = (id: number) => {
    return useQuery({
        queryKey: attachmentKeys.detail(id),
        queryFn: () => attachmentService.getById(id),
        enabled: !!id,
    });
};

export const useCreateAttachment = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: AttachmentRequest) => attachmentService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: attachmentKeys.lists() });
        }
    });
};

export const useUpdateAttachment = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: {id: number; model: AttachmentUpdateRequest}) =>
            attachmentService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: attachmentKeys.lists() });
            queryClient.invalidateQueries({ queryKey: attachmentKeys.detail(variables.id) });
        }
    });
};

export const useDeleteAttachment = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => attachmentService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: attachmentKeys.lists() });
        }
    });
}

export const useAttachmentImage = (filter: AttachmentImgFilter) => {
    return useQuery({
        queryKey: attachmentKeys.imagesList(filter),
        queryFn: () => attachmentImageService.getPaged(filter),
    })
}

export const useUploadAttachmentImage = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ attachmentId, file, description }: { attachmentId: number; file: File; description?: string }) =>
            attachmentImageService.upload(attachmentId, file, description),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({
                queryKey: attachmentKeys.imagesByAttachment(variables.attachmentId),
            });
        },
    });
};

export const useDeleteAttachmentImage = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => attachmentImageService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: attachmentKeys.imagesAll });
        },
    });
};

export const useAttachmentImageFile = (id: number) => {
    return useQuery({
        queryKey: [...attachmentKeys.imagesAll, 'file', id],
        queryFn: () => attachmentImageService.downloadFile(id),
        staleTime: Infinity,
    });
};