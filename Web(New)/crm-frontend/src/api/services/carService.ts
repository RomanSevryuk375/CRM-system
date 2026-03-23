import { apiClient } from "../config.ts";
import type {
    CarFilter,
    CarRequest,
    CarResponse,
    CarStatusResponse,
    CarUpdateRequest
} from "../types/car.ts";

export const carService = {
    getPaged: async (filter: CarFilter) => {
        return (await apiClient.get<CarResponse[]>('/cars', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<CarResponse>(`/cars/${id}`)).data;
    },

    create: async (request: CarRequest) => {
        return (await apiClient.post<CarResponse>('/cars', request)).data;
    },

    update: async (id: number, request: CarUpdateRequest) => {
        return (await apiClient.put(`/cars/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/cars/${id}`)).data;
    }
};

export const carStatusService = {
    getAll: async () => {
        return (await apiClient.get<CarStatusResponse[]>('/car-statuses')).data;
    }
};
