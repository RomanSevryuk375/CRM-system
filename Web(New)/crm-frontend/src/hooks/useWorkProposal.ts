import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type {
    WorkProposalFilter,
    WorkProposalRequest,
    ProposalStatusRequest
} from "../api/types/workProposal.ts";
import { workProposalService, workProposalStatusService } from "../api/services/workProposalService.ts";

export const workProposalKeys = {
    all: ['work-proposals'] as const,
    lists: () => [...workProposalKeys.all, 'list'] as const,
    list: (filter: WorkProposalFilter) => [...workProposalKeys.lists(), filter] as const,
    details: () => [...workProposalKeys.all, 'detail'] as const,
    detail: (id: number) => [...workProposalKeys.details(), id] as const,
    statuses: ['work-proposal-statuses'] as const,
};

export const useWorkProposals = (filter: WorkProposalFilter) => {
    return useQuery({
        queryKey: workProposalKeys.list(filter),
        queryFn: () => workProposalService.getPaged(filter),
        placeholderData: (previousData) => previousData,
    });
};

export const useWorkProposal = (id: number) => {
    return useQuery({
        queryKey: workProposalKeys.detail(id),
        queryFn: () => workProposalService.getById(id),
        enabled: !!id,
    });
};

export const useWorkProposalStatuses = () => {
    return useQuery({
        queryKey: workProposalKeys.statuses,
        queryFn: () => workProposalStatusService.getAll(),
        staleTime: 1000 * 60 * 60,
    });
};

export const useCreateWorkProposal = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (data: WorkProposalRequest) => workProposalService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workProposalKeys.lists() });
        },
    });
};

export const usePatchWorkProposalStatus = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ id, model }: { id: number; model: ProposalStatusRequest }) =>
            workProposalService.patchStatus(id, model),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: workProposalKeys.lists() });
            queryClient.invalidateQueries({ queryKey: workProposalKeys.detail(variables.id) });
        },
    });
};

export const useDeleteWorkProposal = async () => {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (id: number) => workProposalService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: workProposalKeys.lists() });
        },
    });
};