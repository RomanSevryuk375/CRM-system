import { makeRequest } from "./makeRequest";

const URL = "/Specialization";

export const getSpecializations = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
    withCredentials: true,
});

export const createSpecialization = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateSpecialization = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteSpecialization = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});