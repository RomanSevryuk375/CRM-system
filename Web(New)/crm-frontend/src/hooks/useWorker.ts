import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    WorkerFilter,
    WorkerRequest,
    WorkerWithUserRequest,
    WorkerUpdateRequest
} from "../api/types/worker.ts";
import { workerService } from "../api/services/workerService.ts";

export const workerKeys = {
    all: ['workers'] as const,
    lists: () => [...workerKeys.all, 'list'] as const,
    list: (filter: WorkerFilter) => [...workerKeys.lists(), filter] as const,
    details: () => [...workerKeys.all, 'detail'] as const,
    detail: (id: number) => [...workerKeys.details(), id] as const,
};

export const useWorkers = (filter: WorkerFilter) => {
    return useQuery({
        queryKey: workerKeys.list(filter),
        queryFn: () => workerService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useWorker = (id: number) => {
    return useQuery({
        queryKey: workerKeys.detail(id),
        queryFn: () => workerService.getById(id),
        enabled: !!id,
    });
};

export const useCreateWorker = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: WorkerRequest) => workerService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workerKeys.lists() });
        },
    });
};

export const useCreateWorkerWithUser = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: WorkerWithUserRequest) => workerService.createWithUser(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workerKeys.lists() });
        },
    });
};

export const useUpdateWorker = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: WorkerUpdateRequest }) =>
            workerService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: workerKeys.lists() });
            queryClient.invalidateQueries({ queryKey: workerKeys.detail(variables.id) });
        },
    });
};

export const useDeleteWorker = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => workerService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workerKeys.lists() });
        },
    });
};