import { api } from "../../api";
import { getCatalogFailed, getCatalogSuccess, getCatalogStarted, setCatalogTotal, createWorkTypeStarted, createWorkTypeSuccess, createWorkTypeFailed, updateWorkTypeStarted, updateWorkTypeSuccess, updateWorkTypeFailed, deleteWorkTypeStarted, deleteWorkTypeSuccess, deleteWorkTypeFailed } from "../actionCreators/catalogOfWorks"

export const getCatalogOfWorks = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getCatalogStarted());

            const response = await api.catalogOfWorks.getCatalogOfWorks({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });
            
            const totalCount = parseInt(response.headers['x-total-count'], 10);
            if (!isNaN(totalCount)) {
                dispatch(setCatalogTotal(totalCount));
            }

            dispatch(getCatalogSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getCatalogFailed(error));
        }
    };
};

export const createWorkType = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createWorkTypeStarted())

            const response = await api.catalogOfWorks.createWorkType(data)

            dispatch(createWorkTypeSuccess(response.data));
        } catch (error) {
            dispatch(createWorkTypeFailed(error));
        }
    };
};

export const updateWorkType = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateWorkTypeStarted());

            const response = await api.catalogOfWorks.updateWorkType(id, data);

            dispatch(updateWorkTypeSuccess(response.data));
        } catch (error) {
            dispatch(updateWorkTypeFailed(error));
        }
    };
};

export const deleteWorkType = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteWorkTypeStarted());

            const response = await api.catalogOfWorks.deleteWorkType(id);

            dispatch(deleteWorkTypeSuccess(response.data));
        } catch (error) {
            dispatch(deleteWorkTypeFailed(error));
        }
    };
};