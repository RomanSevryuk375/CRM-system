import { api } from "../../api"
import { createWorkFailed, createWorkStarted, createWorkSuccess, deleteWorkFailed, deleteWorkStarted, deleteWorkSuccess, getMyWorksFailed, getMyWorksStarted, getMyWorksSuccess, getWorksWithInfoFailed, getWorksWithInfoStarted, getWorksWithInfoSuccess, setMyWorksTotal, setWorksWithInfoTotal, updateWorkFailed, updateWorkStarted, updateWorkSuccess } from "../actionCreators/works"

export const getWorksWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getWorksWithInfoStarted());

            const response = await api.works.getWorksWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setWorksWithInfoTotal(totalCount));
            };

            dispatch(getWorksWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getWorksWithInfoFailed(error));
        }
    };
};

export const getMyWorks = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getMyWorksStarted())

            const response = await api.works.getMyWorks({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setMyWorksTotal(totalCount));
            };

            dispatch(getMyWorksSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getMyWorksFailed(error));
        }
    };
};

export const createWork = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createWorkStarted());

            const response = await api.works.createWork(data);

            dispatch(createWorkSuccess(response.data));
        } catch (error) {
            dispatch(createWorkFailed(error));
        }
    };
};

export const updateWork = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateWorkStarted())

            const response = await api.works.updateWork(id, data);

            dispatch(updateWorkSuccess(response.data));
        } catch (error) {
            dispatch(updateWorkFailed(error));
        }
    };
};

export const deleteWork = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteWorkStarted());

            const response = await api.works.deleteWork(id);

            dispatch(deleteWorkSuccess(response.data));
        } catch (error) {
            dispatch(deleteWorkFailed(error));
        }
    };
};