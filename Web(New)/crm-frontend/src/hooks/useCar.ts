import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {CarFilter, CarRequest, CarUpdateRequest} from "../api/types/car.ts";
import {carService, carStatusService} from "../api/services/carService.ts";

export const carKeys = {
    all: ['cars'] as const,
    lists: () => [...carKeys.all, 'list'] as const,
    list: (filter: CarFilter) => [...carKeys.lists(), filter] as const,
    details: () => [...carKeys.all, 'detail'] as const,
    detail: (id: number) => [...carKeys.details(), id] as const,
    statuses: ['car-statuses'] as const,
};

export const useCars = (filter: CarFilter) => {
    return useQuery({
        queryKey: carKeys.list(filter),
        queryFn: () => carService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useCar = (id: number) => {
    return useQuery({
        queryKey: carKeys.detail(id),
        queryFn: () => carService.getById(id),
        enabled: !!id,
    });
};

export const useCarStatuses = () => {
    return useQuery({
        queryKey: carKeys.statuses,
        queryFn: () => carStatusService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateCar = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: CarRequest) => carService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: carKeys.lists() });
        },
    });
};

export const useUpdateCar = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: CarUpdateRequest }) =>
            carService.update(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: carKeys.lists() });
            queryClient.invalidateQueries({ queryKey: carKeys.detail(variables.id) });
        },
    });
};

export const useDeleteCar = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => carService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: carKeys.lists() });
        },
    });
};