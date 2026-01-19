import { makeRequest } from "./makeRequest";

const URL = "/WorkProposal";

export const getworkProposalWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
    withCredentials: true,
});

export const getWorkProposalInWork = (config) => makeRequest({
    method: "GET",
    url:`${URL}/InWork`,
    ...config,
    withCredentials: true,
});

export const getWorkProposalForCar = (id, config) => makeRequest({
    method: "GET",
    url: `${URL}/forCar/${id}`,
    ...config,
    withCredentials: true,
})

export const createWorkProposal = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateWorkProposal = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const acceptWorkProposal = (id) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}/accept`,
    withCredentials: true,
});

export const rejectWorkProposal = (id) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}/reject`,
    withCredentials: true,
});

export const deleteWorkProposal = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});