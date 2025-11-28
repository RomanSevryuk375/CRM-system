import { makeRequest } from "./makeRequest";

const URL = "/Status";

export const getStatuses = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});