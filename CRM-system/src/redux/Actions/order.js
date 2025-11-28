import { api } from "../../api"
import { getOrdersMy } from "../../api/orders";
import { createOrderFailed, createOrderStarted, createOrderSuccess, deleteOrderFailed, deleteOrderStarted, deleteOrderSuccess, getOrdersInWorkFailed, getOrdersInWorkStarted, getOrdersInWorkSuccess, getOrdersMyFailed, getOrdersMyStarted, getOrdersMySuccess, getOrdersWithInfoFailed, getOrdersWithInfoStarted, getOrdersWithInfoSuccess, setOrdersInWorkTotal, setOrdersWithInfoTotal, updateOrderFailed, updateOrderStarted, updateOrderSuccess } from "../actionCreators/orders"

export const getOrdersWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getOrdersWithInfoStarted())

            const response = await api.orders.getOdersWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setOrdersWithInfoTotal(totalCount));
            };

            dispatch(getOrdersWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getOrdersWithInfoFailed(error));
        }
    };
};

export const getOrdersInWork = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getOrdersInWorkStarted());

            const response = await api.orders.getOrdersInWork({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setOrdersInWorkTotal(totalCount));
            };

            dispatch(getOrdersInWorkSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getOrdersInWorkFailed(error))
        }
    };
};

export const getMyOrders = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getOrdersMyStarted());

            const response = await api.orders.getOrdersMy({
                params: { 
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setOrdersInWorkTotal(totalCount));
            };

            dispatch(getOrdersMySuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getOrdersMyFailed(error));
        }
    };
};

export const createOrder = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createOrderStarted());

            const response = await api.orders.createOrder(data);

            dispatch(createOrderSuccess(response.data));
        } catch (error) {
            dispatch(createOrderFailed(error));
        }
    };
};

export const updateOrder = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateOrderStarted());

            const response = await api.orders.updateOrder(id, data);

            dispatch(updateOrderSuccess(response.data));
        } catch (error) {
            dispatch(updateOrderFailed(error));
        }
    };
};

export const deleteOrder = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteOrderStarted());

            const response = await api.orders.deleteOrder(id);

            dispatch(deleteOrderSuccess(response.data));
        } catch (error) {
            dispatch(deleteOrderFailed(error));
        }
    };
};