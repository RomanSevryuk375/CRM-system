import { makeRequest } from "./makeRequest";

const URL = "/Client";

export const getUsers = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});