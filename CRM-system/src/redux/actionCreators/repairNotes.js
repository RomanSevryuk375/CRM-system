export const GET_REPAIR_NOTES_WITH_INFO_STARTED = "GET_REPAIR_NOTES_WITH_INFO_STARTED";
export const GET_REPAIR_NOTES_WITH_INFO_SUCCESS = "GET_REPAIR_NOTES_WITH_INFO_SUCCESS";
export const GET_REPAIR_NOTES_WITH_INFO_FAILED = "GET_REPAIR_NOTES_WITH_INFO_FAILED";
export const SET_REPAIR_NOTES_WITH_INFO_TOTAL = "SET_REPAIR_NOTES_WITH_INFO_TOTAL";

export const GET_MY_REPAIR_NOTES_STARTED = "GET_MY_REPAIR_NOTES_STARTED";
export const GET_MY_REPAIR_NOTES_SUCCESS = "GET_MY_REPAIR_NOTES_SUCCESS";
export const GET_MY_REPAIR_NOTES_FAILED = "GET_MY_REPAIR_NOTES_FAILED";
export const SET_MY_REPAIR_NOTES_TOTAL = "SET_MY_REPAIR_NOTES_TOTAL";

export const GET_REPAIR_NOTES_IN_WORK_STARTED = "GET_REPAIR_NOTES_IN_WORK_STARTED";
export const GET_REPAIR_NOTES_IN_WORK_SUCCESS = "GET_REPAIR_NOTES_IN_WORK_SUCCESS";
export const GET_REPAIR_NOTES_IN_WORK_FAILED = "GET_REPAIR_NOTES_IN_WORK_FAILED";
export const SET_REPAIR_NOTES_IN_WORK_TOTAL = "SET_REPAIR_NOTES_IN_WORK_TOTAL";

export const getRepairNotesWithInfoStarted = () => ({
    type: GET_REPAIR_NOTES_WITH_INFO_STARTED,
});
export const getRepairNotesWithInfoSuccess = (repairNotes) => ({
    type: GET_REPAIR_NOTES_WITH_INFO_SUCCESS,
    payload: repairNotes,
});
export const getRepairNotesWithInfoFailed = (error) => ({
    type: GET_REPAIR_NOTES_WITH_INFO_FAILED,
    payload: error,
});
export const setRepairNotesWithInfoTotal = (total) => ({
    type: SET_REPAIR_NOTES_WITH_INFO_TOTAL,
    payload: total,
});

export const getMyRepairNotesStarted = () => ({
    type: GET_MY_REPAIR_NOTES_STARTED,
});
export const getMyRepairNotesSuccess = (repairNotes) => ({
    type: GET_MY_REPAIR_NOTES_SUCCESS,
    payload: repairNotes,
});
export const getMyRepairNotesFailed = (error) => ({
    type: GET_MY_REPAIR_NOTES_FAILED,
    payload: error,
});
export const setMyRepairNotesTotal = (total) => ({
    type: SET_MY_REPAIR_NOTES_TOTAL,
    payload: total,
});

export const getRepairNotesInWorkStarted = () => ({
    type: GET_REPAIR_NOTES_IN_WORK_STARTED,
});
export const getRepairNotesInWorkSuccess = (repairNotes) => ({
    type: GET_REPAIR_NOTES_IN_WORK_SUCCESS,
    payload: repairNotes,
});
export const getRepairNotesInWorkFailed = (error) => ({
    type: GET_REPAIR_NOTES_IN_WORK_FAILED,
    payload: error,
});
export const setRepairNotesInWorkTotal = (total) => ({
    type: SET_REPAIR_NOTES_IN_WORK_TOTAL,
    payload: total,
});
