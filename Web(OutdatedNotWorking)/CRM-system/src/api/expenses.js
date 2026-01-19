import { makeRequest } from "./makeRequest";

const URL = "/Expense";

export const getExpensesWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-Info`,
    ...config,
    withCredentials: true,
});

export const createExpense = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateExpense = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteExpense = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,
});