import { DELETE_ORDER_FAILED, DELETE_ORDER_STARTED, DELETE_ORDER_SUCCESS, GET_ORDERS_IN_WORK_FAILED, GET_ORDERS_IN_WORK_STARTED, GET_ORDERS_IN_WORK_SUCCESS, GET_ORDERS_MY_FAILED, GET_ORDERS_MY_STARTED, GET_ORDERS_MY_SUCCESS, GET_ORDERS_WITH_INFO_FAILED, GET_ORDERS_WITH_INFO_STARTED, GET_ORDERS_WITH_INFO_SUCCESS, POST_ORDER_FAILED, POST_ORDER_STARTED, POST_ORDER_SUCCESS, PUT_ORDER_FAILED, PUT_ORDER_STARTED, PUT_ORDER_SUCCESS, SET_ORDERS_IN_WORK_TOTAL, SET_ORDERS_MY_TOTAL, SET_ORDERS_WITH_INFO_TOTAL } from "../actionCreators/orders";

const initialeState = {
    orders: [],
    myOrders: [],
    inWorkOrders: [],
    orderTotal: 0,
    myOrdersTotal: 0,
    inWorkOrdersTotal: 0,
    isOrdersLoading: true,
};

export const ordersReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_ORDERS_WITH_INFO_STARTED:
            return {
                ...state,
                isOrdersLoading: true,
            };
        case GET_ORDERS_WITH_INFO_SUCCESS:
            return {
                ...state,
                orders: action.payload.page === 1
                    ? action.payload.data
                    : [...state.orders, ...action.payload.data],
                isOrdersLoading: false,
            };
        case GET_ORDERS_WITH_INFO_FAILED:
            return {
                ...state,
                isOrdersLoading: false,
            };
        case SET_ORDERS_WITH_INFO_TOTAL:
            return {
                ...state,
                orderTotal: action.payload,
            };

        case GET_ORDERS_IN_WORK_STARTED:
            return {
                ...state,
                isOrdersLoading: true,
            };
        case GET_ORDERS_IN_WORK_SUCCESS:
            return {
                ...state,
                inWorkOrders: action.payload.page === 1
                    ? action.payload.data
                    : [...state.inWorkOrders, ...action.payload.data],
                isOrdersLoading: false,
            };
        case GET_ORDERS_IN_WORK_FAILED:
            return {
                ...state,
                isOrdersLoading: false,
            };
        case SET_ORDERS_IN_WORK_TOTAL:
            return {
                ...state,
                inWorkOrdersTotal: action.payload,
            };

        case GET_ORDERS_MY_STARTED:
            return {
                ...state,
                isOrdersLoading: true,
            };
        case GET_ORDERS_MY_SUCCESS:
            return {
                ...state,
                myOrders: action.payload.page === 1
                    ? action.payload.data
                    : [...state.myOrders, ...action.payload.data],
                isOrdersLoading: false,
            };
        case GET_ORDERS_MY_FAILED:
            return {
                ...state,
                isOrdersLoading: false,
            };
        case SET_ORDERS_MY_TOTAL:
            return {
                ...state,
                myOrdersTotal: action.payload,
            };

        case POST_ORDER_STARTED:
            return { 
                ...state,
                isOrdersLoading: true,
            };
        case POST_ORDER_SUCCESS:
            return {
                ...state,
                orders: [action.payload, ...state.orders],
                isOrdersLoading: false,
            };
        case POST_ORDER_FAILED:
            return {
                ...state,
                isOrdersLoading: false,
            };

        case PUT_ORDER_STARTED:
            return {
                ...state,
                isOrdersLoading: true,
            };
        case PUT_ORDER_SUCCESS: 
            return { 
                ...state,
                orders: state.orders.map(item => item.id === action.payload.id ? action.payload : item),
                isOrdersLoading: false,
            };
        case PUT_ORDER_FAILED:
            return {
                ...state, 
                isOrdersLoading: false,
            };

        case DELETE_ORDER_STARTED: 
            return {
                ...state,
                isOrdersLoading: true,
            };
        case DELETE_ORDER_SUCCESS: 
            return {
                ...state,
                orders: state.orders.filter(item => item.id !== action.payload),
                isOrdersLoading: false,
            };
        case DELETE_ORDER_FAILED:
            return {
                ...state,
                isOrdersLoading: false,
            };
        
        default:
            return state;
    }
}