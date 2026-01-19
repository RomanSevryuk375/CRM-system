import { DELETE_EXPENSE_FAILED, DELETE_EXPENSE_STARTED, DELETE_EXPENSE_SUCCESS, GET_EXPENSES_WITH_INFO_FAILED, GET_EXPENSES_WITH_INFO_STARTED, GET_EXPENSES_WITH_INFO_SUCCESS, POST_EXPENSE_FAILED, POST_EXPENSE_STARTED, POST_EXPENSE_SUCCESS, PUT_EXPENSE_FAILED, PUT_EXPENSE_STARTED, PUT_EXPENSE_SUCCESS, SET_EXPENSES_WITH_INFO_TOTAL } from "../actionCreators/expenses";

const initialeState = {
    expenses: [],
    isExpensesLoading: true,
    expensesTotal: 0,
};

export const expensesReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_EXPENSES_WITH_INFO_STARTED:
            return {
                ...state,
                isExpensesLoading: true,
            };
        case GET_EXPENSES_WITH_INFO_SUCCESS:
            return {
                ...state,
                expenses: action.payload.page === 1
                    ? action.payload.data
                    : [...state.expenses, ...action.payload.data],
                isExpensesLoading: false,
            };
        case GET_EXPENSES_WITH_INFO_FAILED:
            return {
                ...state,
                isExpensesLoading: false,
            };
        case SET_EXPENSES_WITH_INFO_TOTAL:
            return {
                ...state,
                expensesTotal: action.payload,
            };

        case POST_EXPENSE_STARTED:
            return {
                ...state,
                isExpensesLoading: true,
            };
        case POST_EXPENSE_SUCCESS:
            return {
                ...state,
                expenses: [action.payload, ...state.expenses],
                isExpensesLoading: false,
            };
        case POST_EXPENSE_FAILED:
            return {
                ...state,
                isExpensesLoading: false,
            };

        case PUT_EXPENSE_STARTED:
            return {
                ...state,
                isExpensesLoading: true,
            };
        case PUT_EXPENSE_SUCCESS:
            return {
                ...state,
                expenses: state.expenses.map(item => item.id === action.payload.id ? action.payload : item),
                isExpensesLoading: false,
            };
        case PUT_EXPENSE_FAILED:
            return {
                ...state,
                isExpensesLoading: false,
            };

        case DELETE_EXPENSE_STARTED:
            return {
                ...state,
                isExpensesLoading: true,
            };
        case DELETE_EXPENSE_SUCCESS:
            return {
                ...state,
                expenses: state.expenses.filter(item => item.id !== action.payload),
                isExpensesLoading: false,
            };
        case DELETE_EXPENSE_FAILED:
            return {
                ...state,
                isExpensesLoading: false,
            };

        default:
            return state;
    }
}


