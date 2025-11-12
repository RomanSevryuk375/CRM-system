import { makeRequest } from "./makeRequest";

const URL = "/WorkType";

export const getCatalogOfWorks = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config
});