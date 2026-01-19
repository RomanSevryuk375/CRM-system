export const GET_WORKER_WITH_INFO_STARTED = "GET_WORKER_WITH_INFO_STARTED";
export const GET_WORKER_WITH_INFO_SUCCESS = "GET_WORKER_WITH_INFO_SUCCESS";
export const GET_WORKER_WITH_INFO_FAILED = "GET_WORKER_WITH_INFO_FAILED";
export const SET_WORKER_WITH_INFO_TOTAL = "SET_WORKER_WITH_INFO_TOTAL";

export const GET_MY_WORKER_STARTED = "GET_MY_WORKER_STARTED";
export const GET_MY_WORKER_SUCCESS = "GET_MY_WORKER_SUCCESS";
export const GET_MY_WORKER_FAILED = "GET_MY_WORKER_FAILED";

export const POST_WORKER_STARTED = "POST_WORKER_STARTED";
export const POST_WORKER_SUCCESS = "POST_WORKER_SUCCESS";
export const POST_WORKER_FAILED = "POST_WORKER_FAILED";

export const PUT_WORKER_STARTED = "PUT_WORKER_STARTED";
export const PUT_WORKER_SUCCESS = "PUT_WORKER_SUCCESS";
export const PUT_WORKER_FAILED = "PUT_WORKER_FAILED";

export const DELETE_WORKER_STARTED = "DELETE_WORKER_STARTED";
export const DELETE_WORKER_SUCCESS = "DELETE_WORKER_SUCCESS";
export const DELETE_WORKER_FAILED = "DELETE_WORKER_FAILED";

export const getWorkerWithInfoStarted = () => ({
    type: GET_WORKER_WITH_INFO_STARTED,
});
export const getWorkerWithInfoSuccess = (workers) => ({
    type: GET_WORKER_WITH_INFO_SUCCESS,
    payload: workers,
});
export const getWorkerWithInfoFailed = (error) => ({
    type: GET_WORKER_WITH_INFO_FAILED,
    payload: error,
});
export const setWorkerWithInfoTotal = (total) => ({
    type: SET_WORKER_WITH_INFO_TOTAL,
    payload: total,
});

export const getMyWorkerStarted = () => ({
    type: GET_MY_WORKER_STARTED,
});
export const getMyWorkerSuccess = (worker) => ({
    type: GET_MY_WORKER_SUCCESS,
    payload: worker,
});
export const getMyWorkerFailed = (error) => ({
    type: GET_MY_WORKER_FAILED,
    payload: error,
});

export const createWorkerStarted = () => ({
    type: POST_WORKER_STARTED,
});
export const createWorkerSuccess = (worker) => ({
    type: POST_WORKER_SUCCESS,
    payload: worker,
});
export const createWorkerFailed = (error) => ({
    type: POST_WORKER_FAILED,
    payload: error,
});

export const updateWorkerStarted = () => ({
    type: PUT_WORKER_STARTED,
});
export const updateWorkerSuccess = (worker) => ({
    type: PUT_WORKER_SUCCESS,
    payload: worker,
});
export const updateWorkerFailed = (error) => ({
    type: PUT_WORKER_FAILED,
    payload: error,
});

export const deleteWorkerStarted = () => ({
    type: DELETE_WORKER_STARTED,
});
export const deleteWorkerSuccess = (id) => ({
    type: DELETE_WORKER_SUCCESS,
    payload: id,
});
export const deleteWorkerFailed = (error) => ({
    type: DELETE_WORKER_FAILED,
    payload: error,
});