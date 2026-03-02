import { apiClient } from "../config.ts";
import type { LoginRequest, LoginResponse } from "../types/login.ts";
import type { UserRequest, UserResponse } from "../types/user.ts";

export const userService = {
    getByLogin: async (login: string) => {
        return await apiClient.get<UserResponse>(`/users/${login}`);
    },

    getById: async (id: number) => {
        return await apiClient.get<UserResponse>(`/users/${id}`);
    },

    login: async (request: LoginRequest) => {
        return await apiClient.post<LoginResponse>('/users/token', request);
    },

    logout: async () => {
        return await apiClient.post<{ message: string }>('/users/exit');
    },

    create: async (request: UserRequest) => {
        return await apiClient.post<UserResponse>('/users', request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/users/${id}`);
    }
};