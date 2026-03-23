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
        return (await apiClient.get<WorkProposalResponse[]>('/work-proposals', { params: filter })).data;
    },

    getById: async (id: number) => {
        return (await apiClient.get<WorkProposalResponse>(`/work-proposals/${id}`)).data;
    },

    create: async (request: WorkProposalRequest) => {
        return (await apiClient.post<WorkProposalResponse>('/work-proposals', request)).data;
    },

    patchStatus: async (id: number, request: ProposalStatusRequest) => {
        return (await apiClient.patch(`/work-proposals/${id}`, request)).data;
    },

    delete: async (id: number) => {
        return (await apiClient.delete(`/work-proposals/${id}`)).data;
    }
};

export const workProposalStatusService = {
    getAll: async () => {
        return (await apiClient.get<WorkProposalStatusResponse[]>('/work-proposal-statuses')).data;
    }
};
