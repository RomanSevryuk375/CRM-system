import { makeRequest } from "./makeRequest";

const URL = "/RepairNote";

export const getRepairNotesWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-info`,
    ...config,
});

export const getMyRepairNotes = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
});

export const getRepairNotesInWork = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`
});