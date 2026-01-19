import { makeRequest } from "./makeRequest";

const URL = "/Supplier";

export const getSuppliers = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
    withCredentials: true,
});

export const createSupplier = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateSupplier = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteSupplier = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});