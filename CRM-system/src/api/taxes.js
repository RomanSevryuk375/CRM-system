import { makeRequest } from "./makeRequest";

const URL = "/Tax";

export const getTaxes = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});

export const createTax = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateTax = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteTax = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});