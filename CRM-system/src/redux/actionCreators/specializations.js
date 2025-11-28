export const GET_SPECIALIZATIONS_STARTED = "GET_SPECIALIZATIONS_STARTED";
export const GET_SPECIALIZATIONS_SUCCESS = "GET_SPECIALIZATIONS_SUCCESS";
export const GET_SPECIALIZATIONS_FAILED = "GET_SPECIALIZATIONS_FAILED";
export const SET_SPECIALIZATIONS_TOTAL = "SET_SPECIALIZATIONS_TOTAL";

export const POST_SPECIALIZATION_STARTED = "POST_SPECIALIZATION_STARTED";
export const POST_SPECIALIZATION_SUCCESS = "POST_SPECIALIZATION_SUCCESS";
export const POST_SPECIALIZATION_FAILED = "POST_SPECIALIZATION_FAILED";

export const PUT_SPECIALIZATION_STARTED = "PUT_SPECIALIZATION_STARTED";
export const PUT_SPECIALIZATION_SUCCESS = "PUT_SPECIALIZATION_SUCCESS";
export const PUT_SPECIALIZATION_FAILED = "PUT_SPECIALIZATION_FAILED";

export const DELETE_SPECIALIZATION_STARTED = "DELETE_SPECIALIZATION_STARTED";
export const DELETE_SPECIALIZATION_SUCCESS = "DELETE_SPECIALIZATION_SUCCESS";
export const DELETE_SPECIALIZATION_FAILED = "DELETE_SPECIALIZATION_FAILED";

export const getSpecializationsStarted = () => ({
    type: GET_SPECIALIZATIONS_STARTED,
});
export const getSpecializationsSuccess = (specializations) => ({
    type: GET_SPECIALIZATIONS_SUCCESS,
    payload: specializations,
});
export const getSpecializationsFailed = (error) => ({
    type: GET_SPECIALIZATIONS_FAILED,
    payload: error,
});
export const setSpecializationsTotal = (total) => ({
    type: SET_SPECIALIZATIONS_TOTAL,
    payload: total,
});

export const createSpecializationStarted = () => ({
    type: POST_SPECIALIZATION_STARTED,
});
export const createSpecializationSuccess = (specialization) => ({
    type: POST_SPECIALIZATION_SUCCESS,
    payload: specialization,
});
export const createSpecializationFailed = (error) => ({
    type: POST_SPECIALIZATION_FAILED,
    payload: error,
});

export const updateSpecializationStarted = () => ({
    type: PUT_SPECIALIZATION_STARTED,
});
export const updateSpecializationSuccess = (specialization) => ({
    type: PUT_SPECIALIZATION_SUCCESS,
    payload: specialization,
});
export const updateSpecializationFailed = (error) => ({
    type: PUT_SPECIALIZATION_FAILED,
    payload: error,
});

export const deleteSpecializationStarted = () => ({
    type: DELETE_SPECIALIZATION_STARTED,
});
export const deleteSpecializationSuccess = (id) => ({
    type: DELETE_SPECIALIZATION_SUCCESS,
    payload: id,
});
export const deleteSpecializationFailed = (error) => ({
    type: DELETE_SPECIALIZATION_FAILED,
    payload: error,
});