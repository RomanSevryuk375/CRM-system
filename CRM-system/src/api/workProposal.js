import { makeRequest } from "./makeRequest";

const URL = "/WorkProposal";

export const getworkProposalWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
});

export const getWorkProposalInWork = (config) => makeRequest({
    method: "GET",
    url:`${URL}/InWork`,
    ...config,
});

export const createWorkProposal = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateWorkProposal = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const acceptWorkProposal = (id) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}/accept`,
});

export const rejectWorkProposal = (id) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}/reject`,
});

export const deleteWorkProposal = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});