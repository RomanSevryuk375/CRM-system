import type {
    AcceptanceFilter,
    AcceptanceImgFilter,
    AcceptanceImgResponse,
    AcceptanceRequest,
    AcceptanceResponse,
    AcceptanceUpdateRequest
} from "../types/acceptance.ts";
import {apiClient} from "../config.ts";


export const acceptanceService = {
    getPaged: async (filter: AcceptanceFilter)  => {
        return (await apiClient.get<AcceptanceResponse[]>('/acceptances', { params: filter })).data;
    },

    getById: async (id: number)  => {
        return (await apiClient.get<AcceptanceResponse>(`/acceptances/${id}`)).data;
    },

    create: async (absence: AcceptanceRequest) => {
        return (await apiClient.post(`/acceptances`, absence)).data;
    },

    update: async (id: number, model: AcceptanceUpdateRequest) => {
        return (await apiClient.put(`/acceptances/${id}`, model)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/acceptances/${id}`)).data;
    }
};

export const acceptanceImageService = {
    getPaged: async (filter: AcceptanceImgFilter) => {
        return (await apiClient.get<AcceptanceImgResponse[]>('/acceptance-images', { params: filter })).data;
    },

    upload: async (acceptanceId: number, file: File, description?: string) => {
        const formData = new FormData();
        formData.append('AcceptanceId', acceptanceId.toString());
        formData.append('File', file);
        if (description) formData.append('Description', description);

        const { data } = await apiClient.post<AcceptanceImgResponse>('/acceptance-images', formData, {
            headers: { 'Content-Type': 'multipart/form-data' }
        });
        return data;
    },

    downloadFile: async (id: number): Promise<string> => {
        const response = await apiClient.get(`/acceptance-images/${id}/img`, {
            responseType: 'blob'
        });
        return URL.createObjectURL(response.data);
    },

    delete: async (id: number) => {
        await apiClient.delete(`/acceptance-images/${id}`);
    }
};
