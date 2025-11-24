import { makeRequest } from "./makeRequest";

const URL = "/Order";

export const getOdersWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
});

export const getOdersInWork = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
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
});

export const updateOrder = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteOrder = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});


