import type {
    AbsenceFilter,
    AbsenceRequest,
    AbsenceResponse,
    AbsenceTypeResponse,
    AbsenceUpdateRequest
} from "../types/absence.ts";
import {apiClient} from "../config.ts";

export const absenceService = {
    getPaged: async (filter: AbsenceFilter) => {
        return await apiClient.get<AbsenceResponse[]>('/absence', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get(`/absences/${id}`);
    },

    create: async (absence: AbsenceRequest) => {
        return await apiClient.post(`/absences`, absence);
    },

    update: async (id: number, model: AbsenceUpdateRequest) => {
        return await apiClient.put(`/absences/${id}`, model);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/absences/${id}`);
    }
};

export const absenceTypeService = {

    getAll: async () => {
        return await apiClient.get<AbsenceTypeResponse[]>('/absence-types');
    }

};