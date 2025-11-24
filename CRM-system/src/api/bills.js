import { makeRequest } from "./makeRequest";

const URL = "/Bill";

export const getBills = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});

export const getMyBills = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
});

export const createBill = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

