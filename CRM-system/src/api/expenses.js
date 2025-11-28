import { makeRequest } from "./makeRequest";

const URL = "/Expense";

export const getExpensesWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/with-Info`,
    ...config,
});

export const createExpense = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
});

export const updateExpense = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteExpense = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
});