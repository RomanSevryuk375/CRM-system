import { apiClient } from "../config.ts";
import type {
    SkillFilter,
    SkillRequest,
    SkillResponse
} from "../types/skill.ts";

export const skillService = {
    getPaged: async (filter: SkillFilter) => {
        return (await apiClient.get<SkillResponse[]>('/skills', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<SkillResponse>(`/skills/${id}`)).data;
    },

    create: async (request: SkillRequest) => {
        return (await apiClient.post<SkillResponse>('/skills', request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/skills/${id}`)).data;
    }
};
