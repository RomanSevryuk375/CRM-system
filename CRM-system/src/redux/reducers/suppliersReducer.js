import {GET_SUPPLIERS_STARTED, GET_SUPPLIERS_SUCCESS, GET_SUPPLIERS_FAILED, SET_SUPPLIERS_TOTAL, POST_SUPPLIER_STARTED, POST_SUPPLIER_SUCCESS, POST_SUPPLIER_FAILED, PUT_SUPPLIER_STARTED, PUT_SUPPLIER_SUCCESS, PUT_SUPPLIER_FAILED, DELETE_SUPPLIER_STARTED, DELETE_SUPPLIER_SUCCESS, DELETE_SUPPLIER_FAILED} from "../actionCreators/suppliers";

const initialState = {
    suppliers: [],
    totalSuppliers: 0,
    isSuppliersLoading: true,
    isCreateSupplierLoading: false,
    isUpdateSupplierLoading: false,
    isDeleteSupplierLoading: false,
};

export const suppliersReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_SUPPLIERS_STARTED:
            return {
                ...state,
                isSuppliersLoading: true,
            };

        case GET_SUPPLIERS_SUCCESS:
            return {
                ...state,
                suppliers: action.payload,
                isSuppliersLoading: false,
            };

        case GET_SUPPLIERS_FAILED:
            return {
                ...state,
                isSuppliersLoading: false,
            };

        case SET_SUPPLIERS_TOTAL:
            return {
                ...state,
                totalSuppliers: action.payload,
            };

        case POST_SUPPLIER_STARTED:
            return {
                ...state,
                isCreateSupplierLoading: true,
            };

        case POST_SUPPLIER_SUCCESS:
            return {
                ...state,
                suppliers: [...state.suppliers, action.payload],
                isCreateSupplierLoading: false,
            };

        case POST_SUPPLIER_FAILED:
            return {
                ...state,
                isCreateSupplierLoading: false,
            };

        case PUT_SUPPLIER_STARTED:
            return {
                ...state,
                isUpdateSupplierLoading: true,
            };

        case PUT_SUPPLIER_SUCCESS:
            return {
                ...state,
                suppliers: state.suppliers.map((s) =>
                    s.id === action.payload.id ? action.payload : s
                ),
                isUpdateSupplierLoading: false,
            };

        case PUT_SUPPLIER_FAILED:
            return {
                ...state,
                isUpdateSupplierLoading: false,
            };

        case DELETE_SUPPLIER_STARTED:
            return {
                ...state,
                isDeleteSupplierLoading: true,
            };

        case DELETE_SUPPLIER_SUCCESS:
            return {
                ...state,
                suppliers: state.suppliers.filter((s) => s.id !== action.payload),
                isDeleteSupplierLoading: false,
            };

        case DELETE_SUPPLIER_FAILED:
            return {
                ...state,
                isDeleteSupplierLoading: false,
            };

        default:
            return state;
    }
};
