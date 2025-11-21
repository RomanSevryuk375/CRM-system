import { makeRequest } from "./makeRequest";

const URL = "/UsedPart";

export const getUsedPartsWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
});

export const getUsedPartsInWork = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
});

export const createUsedPart = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateUsedPart = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteUsedPart = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});