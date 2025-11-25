import { makeRequest } from "./makeRequest";

const URL = "/PaymentNote";

export const getPaymentNotes = (config) => makeRequest({
    method: "GET",
    url: URL,
    ...config,
});

export const getMyPaymentNotes = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
});

export const createPaymentNote = (data) => makeRequest ({
    method: "POST",
    url: URL, 
    data,
    withCredentials: true,
});

export const updatePaymentNote = (id, data) => makeRequest ({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deletePaymentNote = (id) => makeRequest ({
    method: "DELETE",
    url: `${URL}/${id}`,
});