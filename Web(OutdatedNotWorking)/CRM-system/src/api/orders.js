import { makeRequest } from "./makeRequest";

const URL = "/Order";

export const getOdersWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
    withCredentials: true,
});

export const getOdersInWork = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
    withCredentials: true,
});

export const getOrdersMy = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
});

export const createOrder = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateOrder = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteOrder = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});


