import { api } from "../../api";
import { createTaxFailed, createTaxStarted, createTaxSuccess, deleteTaxFailed, deleteTaxStarted, deleteTaxSuccess, getTaxesFailed, getTaxesStarted, getTaxesSuccess, setTaxesTotal, updateTaxFailed, updateTaxStarted, updateTaxSuccess } from "../actionCreators/taxes"

export const getTaxes = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getTaxesStarted());

            const response = await api.taxes.getTaxes({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setTaxesTotal(totalCount));
            };

            dispatch(getTaxesSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getTaxesFailed(error));
        }
    };
};

export const createTax = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createTaxStarted());

            const response = await api.taxes.createTax(data);

            dispatch(createTaxSuccess(response.data));
        } catch (error) {
            dispatch(createTaxFailed(error));
        }
    };
};

export const updateTax = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateTaxStarted());

            const response = await api.taxes.updateTax(id, data);

            dispatch(updateTaxSuccess(response.data));
        } catch (error) {
            dispatch(updateTaxFailed(error));
        }
    };
};

export const deleteTax = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteTaxStarted());

            const response = await api.taxes.deleteTax(id);

            dispatch(deleteTaxSuccess(response.data));
        } catch (error) {
            dispatch(deleteTaxFailed(error));
        }
    };
};