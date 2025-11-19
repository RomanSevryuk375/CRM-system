import { api } from "../../api";
import { createExpenseFailed, createExpenseStarted, createExpenseSuccess, deleteExpenseFailed, deleteExpenseStarted, deleteExpenseSuccess, getExpensesWithInfoFailed, getExpensesWithInfoStarted, getExpensesWithInfoSuccess, setExpensesWithInfoTotal, updateExpenseFailed, updateExpenseStarted, updateExpenseSuccess } from "../actionCreators/expenses"

export const getExpensesWithInwo = (page = 1) => {
    return async (dispatch) => { 
        try {
            dispatch(getExpensesWithInfoStarted());

            const response = await api.expenses.getExpensesWithInfo({
                params: { 
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setExpensesWithInfoTotal(totalCount));
            };

            dispatch(getExpensesWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getExpensesWithInfoFailed(error));
        }
    };
};

export const createExpense = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createExpenseStarted());

            const response = await api.expenses.createExpense(data);

            dispatch(createExpenseSuccess(response.data));
        } catch (error) {
            dispatch(createExpenseFailed(error));
        }
    };
};

export const updateExpense = (id, data) => {
    return async (dispatch) => { 
        try {
            dispatch(updateExpenseStarted());

            const response = await api.expenses.updateExpense(id, data);

            dispatch(updateExpenseSuccess(response.data));
        } catch (error) {
            dispatch(updateExpenseFailed(error));
        }
    };
};

export const delteExpense = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteExpenseStarted());

            const response = await api.expenses.deleteExpense(id);

            dispatch(deleteExpenseSuccess(response.data))
        } catch (error) {
            dispatch(deleteExpenseFailed(error));
        }
    };
};