import { makeRequest } from "./makeRequest";

const URL = "/WorkType";

export const getCatalogOfWorks = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config
});

export const createWorkType = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateWorkType = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteWorkType = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});