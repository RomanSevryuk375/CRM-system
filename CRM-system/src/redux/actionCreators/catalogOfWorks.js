export const GET_CATALOG_STARTED = "GET_CATALOG_STARTED";
export const GET_CATALOG_SUCCESS = "GET_CATALOG_SUCCESS";
export const GET_CATALOG_FAILED = "GET_CATALOG_FAILED";
export const SET_CATALOG_TOTAL = "SET_CATALOG_TOTAL";

export const POST_WORK_TYPE_STARTED = "POST_WORK_TYPE_STARTED";
export const POST_WORK_TYPE_SUCCESS = "POST_WORK_TYPE_SUCCESS";
export const POST_WORK_TYPE_FAILED = "POST_WORK_TYPE_FAILED";

export const PUT_WORK_TYPE_STARTED = "PUT_WORK_TYPE_STARTED";
export const PUT_WORK_TYPE_SUCCESS = "PUT_WORK_TYPE_SUCCESS";
export const PUT_WORK_TYPE_FAILED = "PUT_WORK_TYPE_FAILED";

export const DELETE_WORK_TYPE_STARTED = "DELETE_WORK_TYPE_STARTED";
export const DELETE_WORK_TYPE_SUCCESS = "DELETE_WORK_TYPE_SUCCESS";
export const DELETE_WORK_TYPE_FAILED = "DELETE_WORK_TYPE_FAILED";

export const getCatalogStarted = () => ({
    type: GET_CATALOG_STARTED,
});
export const getCatalogSuccess = (workType) => ({
    type: GET_CATALOG_SUCCESS,
    payload: workType,
});
export const getCatalogFailed = (error) => ({
    type: GET_CATALOG_FAILED,
    payload: error,
});
export const setCatalogTotal = (total) => ({
    type: SET_CATALOG_TOTAL,
    payload: total,
});

export const createWorkTypeStarted = () => ({
    type: POST_WORK_TYPE_STARTED,
});
export const createWorkTypeSuccess = (workType) => ({
    type: POST_WORK_TYPE_SUCCESS,
    payload: workType,
});
export const createWorkTypeFailed = (error) => ({
    type: POST_WORK_TYPE_FAILED,
    payload: error,
});

export const updateWorkTypeStarted = () => ({
    type: PUT_WORK_TYPE_STARTED,
});
export const updateWorkTypeSuccess = (workType) => ({
    type: PUT_WORK_TYPE_SUCCESS,
    payload: workType,
});
export const updateWorkTypeFailed = (error) => ({
    type: PUT_WORK_TYPE_FAILED,
    payload: error,
});

export const deleteWorkTypeStarted = () => ({
    type: DELETE_WORK_TYPE_STARTED,
});
export const deleteWorkTypeSuccess = (id) => ({
    type: DELETE_WORK_TYPE_SUCCESS,
    payload: id,
});
export const deleteWorkTypeFailed = (error) => ({
    type: DELETE_WORK_TYPE_FAILED,
    payload: error,
});

