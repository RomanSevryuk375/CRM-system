export const GET_ORDERS_WITH_INFO_STARTED = "GET_ORDERS_WITH_INFO_STARTED";
export const GET_ORDERS_WITH_INFO_SUCCESS = "GET_ORDERS_WITH_INFO_SUCCESS";
export const GET_ORDERS_WITH_INFO_FAILED = "GET_ORDERS_WITH_INFO_FAILED";
export const SET_ORDERS_WITH_INFO_TOTAL = "SET_ORDERS_WITH_INFO_TOTAL";

export const GET_ORDERS_IN_WORK_STARTED = "GET_ORDERS_IN_WORK_STARTED";
export const GET_ORDERS_IN_WORK_SUCCESS = "GET_ORDERS_IN_WORK_SUCCESS";
export const GET_ORDERS_IN_WORK_FAILED = "GET_ORDERS_IN_WORK_FAILED";
export const SET_ORDERS_IN_WORK_TOTAL = "SET_ORDERS_IN_WORK_TOTAL";

export const GET_ORDERS_MY_STARTED = "GET_ORDERS_MY_STARTED";
export const GET_ORDERS_MY_SUCCESS = "GET_ORDERS_MY_SUCCESS";
export const GET_ORDERS_MY_FAILED = "GET_ORDERS_MY_FAILED";
export const SET_ORDERS_MY_TOTAL = "SET_ORDERS_MY_TOTAL";

export const POST_ORDER_STARTED = "POST_ORDER_STARTED";
export const POST_ORDER_SUCCESS = "POST_ORDER_SUCCESS";
export const POST_ORDER_FAILED = "POST_ORDER_FAILED";

export const PUT_ORDER_STARTED = "PUT_ORDER_STARTED";
export const PUT_ORDER_SUCCESS = "PUT_ORDER_SUCCESS";
export const PUT_ORDER_FAILED = "PUT_ORDER_FAILED";

export const DELETE_ORDER_STARTED = "DELETE_ORDER_STARTED";
export const DELETE_ORDER_SUCCESS = "DELETE_ORDER_SUCCESS";
export const DELETE_ORDER_FAILED = "DELETE_ORDER_FAILED";

export const getOrdersWithInfoStarted = () => ({
    type: GET_ORDERS_WITH_INFO_STARTED,
});
export const getOrdersWithInfoSuccess = (orders) => ({
    type: GET_ORDERS_WITH_INFO_SUCCESS,
    payload: orders,
});
export const getOrdersWithInfoFailed = (error) => ({
    type: GET_ORDERS_WITH_INFO_FAILED,
    payload: error,
});
export const setOrdersWithInfoTotal = (total) => ({
    type: SET_ORDERS_WITH_INFO_TOTAL,
    payload: total,
});

export const getOrdersInWorkStarted = () => ({
    type: GET_ORDERS_IN_WORK_STARTED,
});
export const getOrdersInWorkSuccess = (orders) => ({
    type: GET_ORDERS_IN_WORK_SUCCESS,
    payload: orders,
});
export const getOrdersInWorkFailed = (error) => ({
    type: GET_ORDERS_IN_WORK_FAILED,
    payload: error,
});
export const setOrdersInWorkTotal = (total) => ({
    type: SET_ORDERS_IN_WORK_TOTAL,
    payload: total,
});

export const getOrdersMyStarted = () => ({
    type: GET_ORDERS_MY_STARTED,
});
export const getOrdersMySuccess = (orders) => ({
    type: GET_ORDERS_MY_SUCCESS,
    payload: orders,
});
export const getOrdersMyFailed = (error) => ({
    type: GET_ORDERS_MY_FAILED,
    payload: error,
});
export const setOrdersMyTotal = (total) => ({
    type: SET_ORDERS_MY_TOTAL,
    payload: total,
});

export const createOrderStarted = () => ({
    type: POST_ORDER_STARTED,
});
export const createOrderSuccess = (order) => ({
    type: POST_ORDER_SUCCESS,
    payload: order,
});
export const createOrderFailed = (error) => ({
    type: POST_ORDER_FAILED,
    payload: error,
});

export const updateOrderStarted = () => ({
    type: PUT_ORDER_STARTED,
});
export const updateOrderSuccess = (order) => ({
    type: PUT_ORDER_SUCCESS,
    payload: order,
});
export const updateOrderFailed = (error) => ({
    type: PUT_ORDER_FAILED,
    payload: error,
});

export const deleteOrderStarted = () => ({
    type: DELETE_ORDER_STARTED,
});
export const deleteOrderSuccess = (id) => ({
    type: DELETE_ORDER_SUCCESS,
    payload: id,
});
export const deleteOrderFailed = (error) => ({
    type: DELETE_ORDER_FAILED,
    payload: error,
});