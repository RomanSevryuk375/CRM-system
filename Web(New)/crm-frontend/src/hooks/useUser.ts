import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { LoginRequest } from "../api/types/login.ts";
import type { UserRequest } from "../api/types/user.ts";
import { userService } from "../api/services/userService.ts";

export const userKeys = {
    all: ['users'] as const,
    details: () => [...userKeys.all, 'detail'] as const,
    detail: (id: number) => [...userKeys.details(), id] as const,
    byLogin: (login: string) => [...userKeys.all, 'by-login', login] as const,
};

export const useUser = (id: number) => {
    return useQuery({
        queryKey: userKeys.detail(id),
        queryFn: () => userService.getById(id),
        enabled: !!id,
    });
};

export const useUserByLogin = (login: string) => {
    return useQuery({
        queryKey: userKeys.byLogin(login),
        queryFn: () => userService.getByLogin(login),
        enabled: !!login,
    });
};

export const useLogin = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: LoginRequest) => userService.login(data),
        onSuccess: () => {
            queryClient.clear();
        },
    });
};

export const useLogout = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: () => userService.logout(),
        onSuccess: () => {
            queryClient.clear();
        },
    });
};

export const useCreateUser = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: UserRequest) => userService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: userKeys.all });
        },
    });
};

export const useDeleteUser = () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => userService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: userKeys.all });
        },
    });
};