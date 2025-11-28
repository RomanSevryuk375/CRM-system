import { api } from "../../api";
import { createSpecializationFailed, createSpecializationStarted, createSpecializationSuccess, deleteSpecializationFailed, deleteSpecializationStarted, deleteSpecializationSuccess, getSpecializationsFailed, getSpecializationsStarted, getSpecializationsSuccess, setSpecializationsTotal, updateSpecializationFailed, updateSpecializationStarted } from "../actionCreators/specializations"

export const getSpecializations = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getSpecializationsStarted());

            const response = await api.specializations.getSpecializations({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setSpecializationsTotal(totalCount));
            };

            dispatch(getSpecializationsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getSpecializationsFailed(error));
        }
    };
};

export const createSpecialization = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createSpecializationStarted());

            const response = await api.specializations.createSpecialization(data);

            dispatch(createSpecializationSuccess(response.data));
        } catch (error) {
            dispatch(createSpecializationFailed(error));
        }
    };
};

export const updateSpecialization = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateSpecializationStarted());

            const response = await api.specializations.updateSpecialization(id, data);

            dispatch(updateSpecializationStarted(response.data));
        } catch (error) {
            dispatch(updateSpecializationFailed(error));
        }
    };
};

export const deleteSpecialization = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteSpecializationStarted())

            const response = await api.specializations.deleteSpecialization(id);

            dispatch(deleteSpecializationSuccess(response.data));
        } catch (error) {
            dispatch(deleteSpecializationFailed(error));
        }
    };
};