export const GET_EXPENSES_WITH_INFO_STARTED = "GET_EXPENSES_WITH_INFO_STARTED";
export const GET_EXPENSES_WITH_INFO_SUCCESS = "GET_EXPENSES_WITH_INFO_SUCCESS";
export const GET_EXPENSES_WITH_INFO_FAILED = "GET_EXPENSES_WITH_INFO_FAILED";
export const SET_EXPENSES_WITH_INFO_TOTAL = "SET_EXPENSES_WITH_INFO_TOTAL";

export const POST_EXPENSE_STARTED = "POST_EXPENSE_STARTED";
export const POST_EXPENSE_SUCCESS = "POST_EXPENSE_SUCCESS";
export const POST_EXPENSE_FAILED = "POST_EXPENSE_FAILED";

export const PUT_EXPENSE_STARTED = "PUT_EXPENSE_STARTED";
export const PUT_EXPENSE_SUCCESS = "PUT_EXPENSE_SUCCESS";
export const PUT_EXPENSE_FAILED = "PUT_EXPENSE_FAILED";

export const DELETE_EXPENSE_STARTED = "DELETE_EXPENSE_STARTED";
export const DELETE_EXPENSE_SUCCESS = "DELETE_EXPENSE_SUCCESS";
export const DELETE_EXPENSE_FAILED = "DELETE_EXPENSE_FAILED";

export const getExpensesWithInfoStarted = () => ({
    type: GET_EXPENSES_WITH_INFO_STARTED,
});
export const getExpensesWithInfoSuccess = (expenses) => ({
    type: GET_EXPENSES_WITH_INFO_SUCCESS,
    payload: expenses,
});
export const getExpensesWithInfoFailed = (error) => ({
    type: GET_EXPENSES_WITH_INFO_FAILED,
    payload: error,
});
export const setExpensesWithInfoTotal = (total) => ({
    type: SET_EXPENSES_WITH_INFO_TOTAL,
    payload: total,
});

export const createExpenseStarted = () => ({
    type: POST_EXPENSE_STARTED,
});
export const createExpenseSuccess = (expense) => ({
    type: POST_EXPENSE_SUCCESS,
    payload: expense,
});
export const createExpenseFailed = (error) => ({
    type: POST_EXPENSE_FAILED,
    payload: error,
});

export const updateExpenseStarted = () => ({
    type: PUT_EXPENSE_STARTED,
});
export const updateExpenseSuccess = (expense) => ({
    type: PUT_EXPENSE_SUCCESS,
    payload: expense,
});
export const updateExpenseFailed = (error) => ({
    type: PUT_EXPENSE_FAILED,
    payload: error,
});

export const deleteExpenseStarted = () => ({
    type: DELETE_EXPENSE_STARTED,
});
export const deleteExpenseSuccess = (id) => ({
    type: DELETE_EXPENSE_SUCCESS,
    payload: id,
});
export const deleteExpenseFailed = (error) => ({
    type: DELETE_EXPENSE_FAILED,
    payload: error,
});