import { makeRequest } from "./makeRequest";

const URL = "/Supplier";

export const getSuppliers = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});

export const createSupplier = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateSupplier = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteSupplier = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});