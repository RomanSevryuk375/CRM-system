import { api } from "../../api"
import { createUsedPartFailed, createUsedPartStarted, createUsedPartSuccess, deleteUsedPartFailed, deleteUsedPartStarted, deleteUsedPartSuccess, getUsedPartsInWorkFailed, getUsedPartsInWorkStarted, getUsedPartsInWorkSuccess, getUsedPartsWithInfoFailed, getUsedPartsWithInfoStarted, getUsedPartsWithInfoSuccess, setUsedPartsWithInfoTotal, updateUsedPartFailed, updateUsedPartStarted, updateUsedPartSuccess } from "../actionCreators/usedParts"

export const getUsedPartsWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getUsedPartsWithInfoStarted())

            const response = await api.usedParts.getUsedPartsWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setUsedPartsWithInfoTotal(totalCount));
            };

            dispatch(getUsedPartsWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getUsedPartsWithInfoFailed(error));
        }
    };
};

export const getUsedPartsInWork = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getUsedPartsInWorkStarted());

            const response = await api.usedParts.getUsedPartsInWork({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setUsedPartsInWorkTotal(totalCount));
            };

            dispatch(getUsedPartsInWorkSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getUsedPartsInWorkFailed(error));
        }
    };
};

export const createUsedPart = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createUsedPartStarted());

            const response = await api.usedParts.createUsedPart(data);

            dispatch(createUsedPartSuccess(response.data));
        } catch (error) {
            dispatch(createUsedPartFailed(error));
        }
    };
};

export const updateUsedPart = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateUsedPartStarted())

            const response = await api.usedParts.updateUsedPart(id, data);

            dispatch(updateUsedPartSuccess(response.data));
        } catch (error) {
            dispatch(updateUsedPartFailed(error));
        }
    };
};

export const deleteUsedPart = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteUsedPartStarted());

            const response = await api.usedParts.deleteUsedPart(id);

            dispatch(deleteUsedPartSuccess(response.data));
        } catch (error) {
            dispatch(deleteUsedPartFailed(error));
        }
    };
};