import type {
    AttachmentFilter,
    AttachmentImgFilter,
    AttachmentImgResponse,
    AttachmentRequest,
    AttachmentResponse,
    AttachmentUpdateRequest
} from "../types/attachment.ts";
import {apiClient} from "../config.ts";

export const attachmentService = {
    getPaged: async (filter: AttachmentFilter) => {
        return (await apiClient.get<AttachmentResponse[]>(`attachments`, { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get(`/attachments/${id}`)).data;
    },

    create: async (attachment: AttachmentRequest) => {
        return (await apiClient.post(`attachments`, attachment)).data;
    },

    update: async (id: number, attachment: AttachmentUpdateRequest) => {
        return (await apiClient.put(`attachments/${id}`, attachment)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`attachments/${id}`)).data;
    }
};

export const attachmentImageService = {
    getPaged: async (filter: AttachmentImgFilter) => {
        return (await apiClient.get<AttachmentImgResponse[]>('/attachments-images', { params: filter })).data;
    },

    upload: async (attachmentId: number, file: File, description?: string) => {
        const formData = new FormData();
        formData.append('AttachmentId', attachmentId.toString());
        formData.append('File', file);
        if (description) formData.append('Description', description);

        const { data } = await apiClient.post<AttachmentImgResponse>('/attachments-images', formData, {
            headers: { 'Content-Type': 'multipart/form-data' }
        });
        return data;
    },

    downloadFile: async (id: number): Promise<string> => {
        const response = await apiClient.get(`/attachments-images/${id}/download`, {
            responseType: 'blob'
        });
        return URL.createObjectURL(response.data);
    },

    delete: async (id: number) => {
        await apiClient.delete(`/attachments-images/${id}`);
    }
};
