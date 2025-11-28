export const GET_BILLS_STARTED = "GET_BILLS_STARTED";
export const GET_BILLS_SUCCESS = "GET_BILLS_SUCCESS";
export const GET_BILLS_FAILED = "GET_BILLS_FAILED";
export const SET_BILLS_TOTAL = "SET_BILLS_TOTAL";

export const GET_MY_BILLS_STARTED = "GET_MY_BILLS_STARTED";
export const GET_MY_BILLS_SUCCESS = "GET_MY_BILLS_SUCCESS";
export const GET_MY_BILLS_FAILED = "GET_MY_BILLS_FAILED";
export const SET_MY_BILLS_TOTAL = "SET_MY_BILLS_TOTAL";

export const POST_BILL_STARTED = "POST_BILL_STARTED";
export const POST_BILL_SUCCESS = "POST_BILL_SUCCESS";
export const POST_BILL_FAILED = "POST_BILL_FAILED";

export const getBillsStarted = () => ({
    type: GET_BILLS_STARTED,
});
export const getBillsSuccess = (bills) => ({
    type: GET_BILLS_SUCCESS,
    payload: bills,
});
export const getBillsFailed = (error) => ({
    type: GET_BILLS_FAILED,
    payload: error,
});
export const setBillsTotal = (total) => ({
    type: SET_BILLS_TOTAL,
    payload: total,
});

export const getMyBillsStarted = () => ({
    type: GET_MY_BILLS_STARTED,
});
export const getMyBillsSuccess = (bills) => ({
    type: GET_MY_BILLS_SUCCESS,
    payload: bills,
});
export const getMyBillsFailed = (error) => ({
    type: GET_MY_BILLS_FAILED,
    payload: error,
});
export const setMyBillsTotal = (total) => ({
    type: SET_MY_BILLS_TOTAL,
    payload: total,
});

export const createBillStarted = () => ({
    type: POST_BILL_STARTED,
});
export const createBillSuccess = (bill) => ({
    type: POST_BILL_SUCCESS,
    payload: bill,
});
export const createBillFailed = (error) => ({
    type: POST_BILL_FAILED,
    payload: error,
});