export const GET_SUPPLIERS_STARTED = "GET_SUPPLIERS_STARTED";
export const GET_SUPPLIERS_SUCCESS = "GET_SUPPLIERS_SUCCESS";
export const GET_SUPPLIERS_FAILED = "GET_SUPPLIERS_FAILED";
export const SET_SUPPLIERS_TOTAL = "SET_SUPPLIERS_TOTAL";

export const POST_SUPPLIER_STARTED = "POST_SUPPLIER_STARTED";
export const POST_SUPPLIER_SUCCESS = "POST_SUPPLIER_SUCCESS";
export const POST_SUPPLIER_FAILED = "POST_SUPPLIER_FAILED";

export const PUT_SUPPLIER_STARTED = "PUT_SUPPLIER_STARTED";
export const PUT_SUPPLIER_SUCCESS = "PUT_SUPPLIER_SUCCESS";
export const PUT_SUPPLIER_FAILED = "PUT_SUPPLIER_FAILED";

export const DELETE_SUPPLIER_STARTED = "DELETE_SUPPLIER_STARTED";
export const DELETE_SUPPLIER_SUCCESS = "DELETE_SUPPLIER_SUCCESS";
export const DELETE_SUPPLIER_FAILED = "DELETE_SUPPLIER_FAILED";

export const getSuppliersStarted = () => ({
    type: GET_SUPPLIERS_STARTED,
});
export const getSuppliersSuccess = (suppliers) => ({
    type: GET_SUPPLIERS_SUCCESS,
    payload: suppliers,
});
export const getSuppliersFailed = (error) => ({
    type: GET_SUPPLIERS_FAILED,
    payload: error,
});
export const setSuppliersTotal = (total) => ({
    type: SET_SUPPLIERS_TOTAL,
    payload: total,
});

export const createSupplierStarted = () => ({
    type: POST_SUPPLIER_STARTED,
});
export const createSupplierSuccess = (supplier) => ({
    type: POST_SUPPLIER_SUCCESS,
    payload: supplier,
});
export const createSupplierFailed = (error) => ({
    type: POST_SUPPLIER_FAILED,
    payload: error,
});

export const updateSupplierStarted = () => ({
    type: PUT_SUPPLIER_STARTED,
});
export const updateSupplierSuccess = (supplier) => ({
    type: PUT_SUPPLIER_SUCCESS,
    payload: supplier,
});
export const updateSupplierFailed = (error) => ({
    type: PUT_SUPPLIER_FAILED,
    payload: error,
});

export const deleteSupplierStarted = () => ({
    type: DELETE_SUPPLIER_STARTED,
});
export const deleteSupplierSuccess = (id) => ({
    type: DELETE_SUPPLIER_SUCCESS,
    payload: id,
});
export const deleteSupplierFailed = (error) => ({
    type: DELETE_SUPPLIER_FAILED,
    payload: error,
});