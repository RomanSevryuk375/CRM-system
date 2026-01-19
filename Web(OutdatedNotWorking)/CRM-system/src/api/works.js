import { makeRequest } from "./makeRequest";

const URL = "/Work";

export const getWorksWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
    withCredentials: true,
});

export const getMyWorks = (config) => makeRequest({
    method: "GET",
    url: `${URL}/MyWorks`,
    ...config,
    withCredentials: true,
});

export const getWorksForCar = (id, config) => makeRequest({
    method: "GET",
    url: `${URL}/forCar/${id}`,
    ...config,
    withCredentials: true,
})

export const createWork = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateWork = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteWork = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});

