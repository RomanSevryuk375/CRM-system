import { makeRequest } from "./makeRequest";

const URL = "/User";

export const loginUser = (config) => makeRequest({
    method: "POST",
    url: `${URL}/login`,
    withCredentials: true,
    ...config,
    withCredentials: true,
});

export const logoutUser = (config) => makeRequest({
    method: "POST",
    url: `${URL}/logout`,
    withCredentials: true,
    ...config,
    withCredentials: true,
});

export const createUser = (config) => makeRequest({
    method: "POST",
    url: URL,
    withCredentials: true,
    ...config,
    withCredentials: true,
});

export const deleteUser = (id, config) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
    ...config,
    withCredentials: true,
});