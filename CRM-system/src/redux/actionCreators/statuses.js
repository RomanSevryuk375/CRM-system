export const GET_STATUSES_STARTED = "GET_STATUSES_STARTED";
export const GET_STATUSES_SUCCESS = "GET_STATUSES_SUCCESS";
export const GET_STATUSES_FAILED = "GET_STATUSES_FAILED";
export const SET_STATUSES_TOTAL = "SET_STATUSES_TOTAL";

export const getStatusesStarted = () => ({
    type: GET_STATUSES_STARTED,
});
export const getStatusesSuccess = (statuses) => ({
    type: GET_STATUSES_SUCCESS,
    payload: statuses,
});
export const getStatusesFailed = (error) => ({
    type: GET_STATUSES_FAILED,
    payload: error,
});
export const setStatusesTotal = (total) => ({
    type: SET_STATUSES_TOTAL,
    payload: total,
});