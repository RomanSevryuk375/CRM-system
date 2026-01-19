export const GET_USED_PARTS_WITH_INFO_STARTED = "GET_USED_PARTS_WITH_INFO_STARTED";
export const GET_USED_PARTS_WITH_INFO_SUCCESS = "GET_USED_PARTS_WITH_INFO_SUCCESS";
export const GET_USED_PARTS_WITH_INFO_FAILED = "GET_USED_PARTS_WITH_INFO_FAILED";
export const SET_USED_PARTS_WITH_INFO_TOTAL = "SET_USED_PARTS_WITH_INFO_TOTAL";

export const GET_USED_PARTS_IN_WORK_STARTED = "GET_USED_PARTS_IN_WORK_STARTED";
export const GET_USED_PARTS_IN_WORK_SUCCESS = "GET_USED_PARTS_IN_WORK_SUCCESS";
export const GET_USED_PARTS_IN_WORK_FAILED = "GET_USED_PARTS_IN_WORK_FAILED";
export const SET_USED_PARTS_IN_WORK_TOTAL = "SET_USED_PARTS_IN_WORK_TOTAL";

export const POST_USED_PART_STARTED = "POST_USED_PART_STARTED";
export const POST_USED_PART_SUCCESS = "POST_USED_PART_SUCCESS";
export const POST_USED_PART_FAILED = "POST_USED_PART_FAILED";

export const PUT_USED_PART_STARTED = "PUT_USED_PART_STARTED";
export const PUT_USED_PART_SUCCESS = "PUT_USED_PART_SUCCESS";
export const PUT_USED_PART_FAILED = "PUT_USED_PART_FAILED";

export const DELETE_USED_PART_STARTED = "DELETE_USED_PART_STARTED";
export const DELETE_USED_PART_SUCCESS = "DELETE_USED_PART_SUCCESS";
export const DELETE_USED_PART_FAILED = "DELETE_USED_PART_FAILED";

export const getUsedPartsWithInfoStarted = () => ({
    type: GET_USED_PARTS_WITH_INFO_STARTED,
});
export const getUsedPartsWithInfoSuccess = (usedParts) => ({
    type: GET_USED_PARTS_WITH_INFO_SUCCESS,
    payload: usedParts,
});
export const getUsedPartsWithInfoFailed = (error) => ({
    type: GET_USED_PARTS_WITH_INFO_FAILED,
    payload: error,
});
export const setUsedPartsWithInfoTotal = (total) => ({
    type: SET_USED_PARTS_WITH_INFO_TOTAL,
    payload: total,
});

export const getUsedPartsInWorkStarted = () => ({
    type: GET_USED_PARTS_IN_WORK_STARTED,
});
export const getUsedPartsInWorkSuccess = (usedParts) => ({
    type: GET_USED_PARTS_IN_WORK_SUCCESS,
    payload: usedParts,
});
export const getUsedPartsInWorkFailed = (error) => ({
    type: GET_USED_PARTS_IN_WORK_FAILED,
    payload: error,
});
export const setUsedPartsInWorkTotal = (total) => ({
    type: SET_USED_PARTS_IN_WORK_TOTAL,
    payload: total,
});

export const createUsedPartStarted = () => ({
    type: POST_USED_PART_STARTED,
});
export const createUsedPartSuccess = (usedPart) => ({
    type: POST_USED_PART_SUCCESS,
    payload: usedPart,
});
export const createUsedPartFailed = (error) => ({
    type: POST_USED_PART_FAILED,
    payload: error,
});

export const updateUsedPartStarted = () => ({
    type: PUT_USED_PART_STARTED,
});
export const updateUsedPartSuccess = (usedPart) => ({
    type: PUT_USED_PART_SUCCESS,
    payload: usedPart,
});
export const updateUsedPartFailed = (error) => ({
    type: PUT_USED_PART_FAILED,
    payload: error,
});

export const deleteUsedPartStarted = () => ({
    type: DELETE_USED_PART_STARTED,
});
export const deleteUsedPartSuccess = (id) => ({
    type: DELETE_USED_PART_SUCCESS,
    payload: id,
});
export const deleteUsedPartFailed = (error) => ({
    type: DELETE_USED_PART_FAILED,
    payload: error,
});