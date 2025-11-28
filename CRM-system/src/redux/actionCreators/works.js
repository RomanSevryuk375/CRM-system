export const GET_WORKS_WITH_INFO_STARTED = "GET_WORKS_WITH_INFO_STARTED";
export const GET_WORKS_WITH_INFO_SUCCESS = "GET_WORKS_WITH_INFO_SUCCESS";
export const GET_WORKS_WITH_INFO_FAILED = "GET_WORKS_WITH_INFO_FAILED";
export const SET_WORKS_WITH_INFO_TOTAL = "SET_WORKS_WITH_INFO_TOTAL";

export const GET_MY_WORKS_STARTED = "GET_MY_WORKS_STARTED";
export const GET_MY_WORKS_SUCCESS = "GET_MY_WORKS_SUCCESS";
export const GET_MY_WORKS_FAILED = "GET_MY_WORKS_FAILED";
export const SET_MY_WORKS_TOTAL = "SET_MY_WORKS_TOTAL";

export const POST_WORK_STARTED = "POST_WORK_STARTED";
export const POST_WORK_SUCCESS = "POST_WORK_SUCCESS";
export const POST_WORK_FAILED = "POST_WORK_FAILED";

export const PUT_WORK_STARTED = "PUT_WORK_STARTED";
export const PUT_WORK_SUCCESS = "PUT_WORK_SUCCESS";
export const PUT_WORK_FAILED = "PUT_WORK_FAILED";

export const DELETE_WORK_STARTED = "DELETE_WORK_STARTED";
export const DELETE_WORK_SUCCESS = "DELETE_WORK_SUCCESS";
export const DELETE_WORK_FAILED = "DELETE_WORK_FAILED";

export const getWorksWithInfoStarted = () => ({
    type: GET_WORKS_WITH_INFO_STARTED,
});
export const getWorksWithInfoSuccess = (works) => ({
    type: GET_WORKS_WITH_INFO_SUCCESS,
    payload: works,
});
export const getWorksWithInfoFailed = (error) => ({
    type: GET_WORKS_WITH_INFO_FAILED,
    payload: error,
});
export const setWorksWithInfoTotal = (total) => ({
    type: SET_WORKS_WITH_INFO_TOTAL,
    payload: total,
});

export const getMyWorksStarted = () => ({
    type: GET_MY_WORKS_STARTED,
});
export const getMyWorksSuccess = (works) => ({
    type: GET_MY_WORKS_SUCCESS,
    payload: works,
});
export const getMyWorksFailed = (error) => ({
    type: GET_MY_WORKS_FAILED,
    payload: error,
});
export const setMyWorksTotal = (total) => ({
    type: SET_MY_WORKS_TOTAL,
    payload: total,
});

export const createWorkStarted = () => ({
    type: POST_WORK_STARTED,
});
export const createWorkSuccess = (work) => ({
    type: POST_WORK_SUCCESS,
    payload: work,
});
export const createWorkFailed = (error) => ({
    type: POST_WORK_FAILED,
    payload: error,
});

export const updateWorkStarted = () => ({
    type: PUT_WORK_STARTED,
});
export const updateWorkSuccess = (work) => ({
    type: PUT_WORK_SUCCESS,
    payload: work,
});
export const updateWorkFailed = (error) => ({
    type: PUT_WORK_FAILED,
    payload: error,
});

export const deleteWorkStarted = () => ({
    type: DELETE_WORK_STARTED,
});
export const deleteWorkSuccess = (id) => ({
    type: DELETE_WORK_SUCCESS,
    payload: id,
});
export const deleteWorkFailed = (error) => ({
    type: DELETE_WORK_FAILED,
    payload: error,
});