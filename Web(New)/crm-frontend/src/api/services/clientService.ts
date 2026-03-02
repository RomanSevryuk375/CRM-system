import { apiClient } from "../config.ts";
import type {
    ClientFilter,
    ClientRequest,
    ClientRegisterRequest,
    ClientUpdateRequest,
    ClientResponse,
    RoleResponse
} from "../types/client.ts";

export const clientService = {
    getPaged: async (filter: ClientFilter) => {
        return await apiClient.get<ClientResponse[]>('/clients', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<ClientResponse>(`/clients/${id}`);
    },

    create: async (request: ClientRequest) => {
        return await apiClient.post<ClientResponse>('/clients', request);
    },

    createWithUser: async (request: ClientRegisterRequest) => {
        return await apiClient.post<ClientResponse>('/clients/users', request);
    },

    update: async (id: number, request: ClientUpdateRequest) => {
        return await apiClient.put(`/clients/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/clients/${id}`);
    }
};

export const roleService = {
    getAll: async () => {
        return await apiClient.get<RoleResponse[]>('/roles');
    }
};