import { api } from "../../api";
import { createWorkerFailed, createWorkerStarted, createWorkerSuccess, deleteWorkerFailed, deleteWorkerStarted, deleteWorkerSuccess, getMyWorkerFailed, getMyWorkerStarted, getMyWorkerSuccess, getWorkerWithInfoFailed, getWorkerWithInfoStarted, getWorkerWithInfoSuccess, setWorkerWithInfoTotal, updateWorkerStarted, updateWorkerSuccess } from "../actionCreators/workers"

export const getWorkerWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getWorkerWithInfoStarted());

            const response = await api.workers.getWorkerWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setWorkerWithInfoTotal(totalCount));
            };

            dispatch(getWorkerWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getWorkerWithInfoFailed(error));
        }
    };
};

export const getMyWorker = () => {
    return async (dispatch) => {
        try {
            dispatch(getMyWorkerStarted());

            const response = await api.workers.getMyWorker();

            dispatch(getMyWorkerSuccess(response.data));
        } catch (error) {
            dispatch(getMyWorkerFailed(error));
        }
    };
};

export const createWorker = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createWorkerStarted());

            const response = await api.workers.createWorker(data);

            dispatch(createWorkerSuccess(response.data));
        } catch (error) {
            dispatch(createWorkerFailed(error));
        }
    };
};

export const updateWorker = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateWorkerStarted());

            const response = await api.workers.updateWorker(id, data);

            dispatch(updateWorkerSuccess(response.data));
        } catch (error) {
            dispatch(updateWorkerFailed(error));
        }
    };
};

export const deleteWorker = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteWorkerStarted());

            const response = await api.workers.deleteWorker(id);

            dispatch(deleteWorkerSuccess(response.data));
        } catch (error) {
            dispatch(deleteWorkerFailed(error));
        }
    };
};