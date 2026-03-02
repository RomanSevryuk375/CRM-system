import { apiClient } from "../config.ts";
import type {
    WorkProposalFilter,
    WorkProposalRequest,
    WorkProposalResponse,
    ProposalStatusRequest,
    WorkProposalStatusResponse
} from "../types/workProposal.ts";

export const workProposalService = {
    getPaged: async (filter: WorkProposalFilter) => {
        return await apiClient.get<WorkProposalResponse[]>('/work-proposals', { params: filter });
    },

    getById: async (id: number) => {
        return await apiClient.get<WorkProposalResponse>(`/work-proposals/${id}`);
    },

    create: async (request: WorkProposalRequest) => {
        return await apiClient.post<WorkProposalResponse>('/work-proposals', request);
    },

    patchStatus: async (id: number, request: ProposalStatusRequest) => {
        return await apiClient.patch(`/work-proposals/${id}`, request);
    },

    delete: async (id: number) => {
        return await apiClient.delete(`/work-proposals/${id}`);
    }
};

export const workProposalStatusService = {
    getAll: async () => {
        return await apiClient.get<WorkProposalStatusResponse[]>('/work-proposal-statuses');
    }
};