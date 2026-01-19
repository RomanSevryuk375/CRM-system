import { makeRequest } from "./makeRequest";

const URL = "/UsedPart";

export const getUsedPartsWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
    withCredentials: true,
});

export const getUsedPartsInWork = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
    withCredentials: true,
});

export const createUsedPart = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateUsedPart = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteUsedPart = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});