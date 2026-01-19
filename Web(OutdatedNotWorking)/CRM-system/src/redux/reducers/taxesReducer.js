import {
    GET_TAXES_STARTED,
    GET_TAXES_SUCCESS,
    GET_TAXES_FAILED,
    SET_TAXES_TOTAL,
    POST_TAX_STARTED,
    POST_TAX_SUCCESS,
    POST_TAX_FAILED,
    PUT_TAX_STARTED,
    PUT_TAX_SUCCESS,
    PUT_TAX_FAILED,
    DELETE_TAX_STARTED,
    DELETE_TAX_SUCCESS,
    DELETE_TAX_FAILED
} from "../actionCreators/taxes";

const initialState = {
    taxes: [],
    totalTaxes: 0,
    isTaxesLoading: true,
    isCreateTaxLoading: false,
    isUpdateTaxLoading: false,
    isDeleteTaxLoading: false,
};

export const taxesReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_TAXES_STARTED:
            return {
                ...state,
                isTaxesLoading: true,
            };

        case GET_TAXES_SUCCESS:
            return {
                ...state,
                taxes: action.payload.page === 1
                    ? action.payload.data
                    : [...state.taxes, ...action.payload.data],
                isTaxesLoading: false,
            };

        case GET_TAXES_FAILED:
            return {
                ...state,
                isTaxesLoading: false,
            };

        case SET_TAXES_TOTAL:
            return {
                ...state,
                totalTaxes: action.payload,
            };

        case POST_TAX_STARTED:
            return {
                ...state,
                isCreateTaxLoading: true,
            };

        case POST_TAX_SUCCESS:
            return {
                ...state,
                taxes: [...state.taxes, action.payload],
                isCreateTaxLoading: false,
            };

        case POST_TAX_FAILED:
            return {
                ...state,
                isCreateTaxLoading: false,
            };

        case PUT_TAX_STARTED:
            return {
                ...state,
                isUpdateTaxLoading: true,
            };

        case PUT_TAX_SUCCESS:
            return {
                ...state,
                taxes: state.taxes.map((t) =>
                    t.id === action.payload.id ? action.payload : t
                ),
                isUpdateTaxLoading: false,
            };

        case PUT_TAX_FAILED:
            return {
                ...state,
                isUpdateTaxLoading: false,
            };

        case DELETE_TAX_STARTED:
            return {
                ...state,
                isDeleteTaxLoading: true,
            };

        case DELETE_TAX_SUCCESS:
            return {
                ...state,
                taxes: state.taxes.filter((t) => t.id !== action.payload),
                isDeleteTaxLoading: false,
            };

        case DELETE_TAX_FAILED:
            return {
                ...state,
                isDeleteTaxLoading: false,
            };

        default:
            return state;
    }
};
