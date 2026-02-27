import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {ClientFilter, ClientRegisterRequest, ClientRequest, ClientUpdateRequest} from "../api/types/client.ts";
import {clientService, roleService} from "../api/services/clientService.ts";

export const clientKeys = {
    all: ['clients'] as const,
    lists: () => [...clientKeys.all, 'list'] as const,
    list: (filter: ClientFilter) => [...clientKeys.lists(), filter] as const,
    details: () => [...clientKeys.all, 'detail'] as const,
    detail: (id: number) => [...clientKeys.details(), id] as const,
    roles: ['roles'] as const,
};

export const useClients = (filter: ClientFilter) => {
    return useQuery({
        queryKey: clientKeys.list(filter),
        queryFn: () => clientService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useClient = (id: number) => {
    return useQuery({
        queryKey: clientKeys.detail(id),
        queryFn: () => clientService.getById(id),
        enabled: !!id,
    });
};

export const useRoles = () => {
    return useQuery({
        queryKey: clientKeys.roles,
        queryFn: () => roleService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateClient = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ClientRequest) => clientService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: clientKeys.lists() });
        },
    });
};

export const useRegisterClient = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: ClientRegisterRequest) => clientService.createWithUser(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: clientKeys.lists() });
        },
    });
};

export const useUpdateClient = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: ClientUpdateRequest }) =>
            clientService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: clientKeys.lists() });
            queryClient.invalidateQueries({ queryKey: clientKeys.detail(variables.id) });
        },
    });
};

export const useDeleteClient = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => clientService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: clientKeys.lists() });
        },
    });
};