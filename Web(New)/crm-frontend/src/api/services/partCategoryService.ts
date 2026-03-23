import { apiClient } from "../config.ts";
import type {
    PartCategoryRequest,
    PartCategoryResponse,
    PartCategoryUpdateRequest
} from "../types/partCategory.ts";

export const partCategoryService = {
    getAll: async () => {
        return (await apiClient.get<PartCategoryResponse[]>('/part-categories')).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<PartCategoryResponse>(`/part-categories/${id}`)).data;
    },

    create: async (request: PartCategoryRequest) => {
        return (await apiClient.post<PartCategoryResponse>('/part-categories', request)).data;
    },

    update: async (id: number, request: PartCategoryUpdateRequest) => {
        return (await apiClient.put(`/part-categories/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/part-categories/${id}`)).data;
    }
};
