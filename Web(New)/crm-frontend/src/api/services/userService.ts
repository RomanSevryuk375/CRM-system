import { apiClient } from "../config.ts";
import type { LoginRequest, LoginResponse } from "../types/login.ts";
import type { UserRequest, UserResponse } from "../types/user.ts";

export const userService = {
    getByLogin: async (login: string) => {
        return (await apiClient.get<UserResponse>(`/users/${login}`)).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<UserResponse>(`/users/${id}`)).data;
    },

    login: async (request: LoginRequest) => {
        return (await apiClient.post<LoginResponse>('/users/token', request)).data;
    },

    logout: async () => {
        return (await apiClient.post<{ message: string }>('/users/exit')).data;
    },

    create: async (request: UserRequest) => {
        return (await apiClient.post<UserResponse>('/users', request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/users/${id}`)).data;
    }
};
