import { makeRequest } from "./makeRequest";

const URL = "/Client";

export const getClients = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});

export const getMyClient = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
})

export const createClient = (data) => makeRequest({
    method: "POST",
    url: `${URL}/with-user`,
    data,
});

export const updateClient = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

