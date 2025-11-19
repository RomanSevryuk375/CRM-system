export const GET_STATUSES_STARTED = "GET_STATUSES_STARTED";
export const GET_STATUSES_SUCCESS = "GET_STATUSES_SUCCESS";
export const GET_STATUSES_FAILED = "GET_STATUSES_FAILED";


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
