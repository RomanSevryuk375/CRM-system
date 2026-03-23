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
        return (await apiClient.get<ClientResponse[]>('/clients', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<ClientResponse>(`/clients/${id}`)).data;
    },

    create: async (request: ClientRequest) => {
        return (await apiClient.post<ClientResponse>('/clients', request)).data;
    },

    createWithUser: async (request: ClientRegisterRequest) => {
        return (await apiClient.post<ClientResponse>('/clients/users', request)).data;
    },

    update: async (id: number, request: ClientUpdateRequest) => {
        return (await apiClient.put(`/clients/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/clients/${id}`)).data;
    }
};

export const roleService = {
    getAll: async () => {
        return (await apiClient.get<RoleResponse[]>('/roles')).data;
    }
};
