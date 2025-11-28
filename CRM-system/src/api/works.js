import { makeRequest } from "./makeRequest";

const URL = "/Work";

export const getWorksWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
});

export const getMyWorks = (config) => makeRequest({
    method: "GET",
    url: `${URL}/MyWorks`,
    ...config,
});

export const createWork = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateWork = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteWork = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});

