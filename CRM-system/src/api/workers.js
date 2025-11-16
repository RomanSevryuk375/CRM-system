import { makeRequest } from "./makeRequest";

const URL = "/Worker";

export const getWorkerWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config
});

export const getMyWorker = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config
});

export const createWorker = (data) => makeRequest({
    method: "POST",
    url: `${URL}/with-user`,
    data,
});

export const updateWorker = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteWorker = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});