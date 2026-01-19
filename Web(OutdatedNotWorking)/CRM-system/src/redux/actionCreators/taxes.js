export const GET_TAXES_STARTED = "GET_TAXES_STARTED";
export const GET_TAXES_SUCCESS = "GET_TAXES_SUCCESS";
export const GET_TAXES_FAILED = "GET_TAXES_FAILED";
export const SET_TAXES_TOTAL = "SET_TAXES_TOTAL";

export const POST_TAX_STARTED = "POST_TAX_STARTED";
export const POST_TAX_SUCCESS = "POST_TAX_SUCCESS";
export const POST_TAX_FAILED = "POST_TAX_FAILED";

export const PUT_TAX_STARTED = "PUT_TAX_STARTED";
export const PUT_TAX_SUCCESS = "PUT_TAX_SUCCESS";
export const PUT_TAX_FAILED = "PUT_TAX_FAILED";

export const DELETE_TAX_STARTED = "DELETE_TAX_STARTED";
export const DELETE_TAX_SUCCESS = "DELETE_TAX_SUCCESS";
export const DELETE_TAX_FAILED = "DELETE_TAX_FAILED";

export const getTaxesStarted = () => ({
    type: GET_TAXES_STARTED,
});
export const getTaxesSuccess = (taxes) => ({
    type: GET_TAXES_SUCCESS,
    payload: taxes,
});
export const getTaxesFailed = (error) => ({
    type: GET_TAXES_FAILED,
    payload: error,
});
export const setTaxesTotal = (total) => ({
    type: SET_TAXES_TOTAL,
    payload: total,
});

export const createTaxStarted = () => ({
    type: POST_TAX_STARTED,
});
export const createTaxSuccess = (tax) => ({
    type: POST_TAX_SUCCESS,
    payload: tax,
});
export const createTaxFailed = (error) => ({
    type: POST_TAX_FAILED,
    payload: error,
});

export const updateTaxStarted = () => ({
    type: PUT_TAX_STARTED,
});
export const updateTaxSuccess = (tax) => ({
    type: PUT_TAX_SUCCESS,
    payload: tax,
});
export const updateTaxFailed = (error) => ({
    type: PUT_TAX_FAILED,
    payload: error,
});

export const deleteTaxStarted = () => ({
    type: DELETE_TAX_STARTED,
});
export const deleteTaxSuccess = (id) => ({
    type: DELETE_TAX_SUCCESS,
    payload: id,
});
export const deleteTaxFailed = (error) => ({
    type: DELETE_TAX_FAILED,
    payload: error,
});