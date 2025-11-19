import { api } from "../../api";
import { createSupplierFailed, createSupplierStarted, createSupplierSuccess, deleteSupplierFailed, deleteSupplierStarted, deleteSupplierSuccess, getSuppliersFailed, getSuppliersStarted, getSuppliersSuccess, setSuppliersTotal, updateSupplierStarted, updateSupplierSuccess } from "../actionCreators/suppliers"

export const getSuppliers = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getSuppliersStarted());

            const response = await api.suppliers.getSuppliers({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setSuppliersTotal(totalCount));
            };

            dispatch(getSuppliersSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getSuppliersFailed(error));
        }
    };
};

export const createSupplier = (data) => {
    return async (dispatch) => { 
        try {
            dispatch(createSupplierStarted());

            const response = await api.suppliers.createSupplier(data);

            dispatch(createSupplierSuccess(response.data));
        } catch (error) {
            dispatch(createSupplierFailed(error));
        }
    };
};

export const updateSupplier = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateSupplierStarted());

            const response = await api.suppliers.updateSupplier(id, data);

            dispatch(updateSupplierSuccess(response.data));
        } catch(error) {
            dispatch(updateSupplierFailed(error));
        }
    };
};

export const deleteSupplier = (id) => {
    return async (dispatch) => {
        try { 
            dispatch(deleteSupplierStarted());

            const response = await api.suppliers.deleteSupplier(id);

            dispatch(deleteSupplierSuccess(response.data));
        } catch (error) {
            dispatch(deleteSupplierFailed(error));
        }
    };
};