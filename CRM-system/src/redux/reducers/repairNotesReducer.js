import {GET_REPAIR_NOTES_WITH_INFO_STARTED, GET_REPAIR_NOTES_WITH_INFO_SUCCESS, GET_REPAIR_NOTES_WITH_INFO_FAILED, SET_REPAIR_NOTES_WITH_INFO_TOTAL, GET_MY_REPAIR_NOTES_STARTED, GET_MY_REPAIR_NOTES_SUCCESS, GET_MY_REPAIR_NOTES_FAILED, SET_MY_REPAIR_NOTES_TOTAL, GET_REPAIR_NOTES_IN_WORK_STARTED, GET_REPAIR_NOTES_IN_WORK_SUCCESS, GET_REPAIR_NOTES_IN_WORK_FAILED, SET_REPAIR_NOTES_IN_WORK_TOTAL} from "../actionCreators/repairNotes";

const initialState = {
    repairNotes: [],
    myRepairNotes: [],
    repairNotesInWork: [],
    repairNotesTotal: 0,
    myRepairNotesTotal: 0,
    repairNotesInWorkTotal: 0,
    isRepairNotesLoading: true,
};

export const repairNotesReducer = (state = initialState, action) => {
    switch (action.type) {
        case GET_REPAIR_NOTES_WITH_INFO_STARTED:
            return {
                ...state,
                isRepairNotesLoading: true,
            };

        case GET_REPAIR_NOTES_WITH_INFO_SUCCESS:
            return {
                ...state,
                repairNotes: action.payload.page === 1
                    ? action.payload.data
                    : [...state.repairNotes, ...action.payload.data],
                isRepairNotesLoading: false,
            };

        case GET_REPAIR_NOTES_WITH_INFO_FAILED:
            return {
                ...state,
                isRepairNotesLoading: false,
            };

        case SET_REPAIR_NOTES_WITH_INFO_TOTAL:
            return {
                ...state,
                repairNotesTotal: action.payload,
            };

        case GET_MY_REPAIR_NOTES_STARTED:
            return {
                ...state,
                isRepairNotesLoading: true,
            };

        case GET_MY_REPAIR_NOTES_SUCCESS:
            return {
                ...state,
                myRepairNotes: action.payload.page === 1
                    ? action.payload.data
                    : [...state.myRepairNotes, ...action.payload.data],
                isRepairNotesLoading: false,
            };

        case GET_MY_REPAIR_NOTES_FAILED:
            return {
                ...state,
                isRepairNotesLoading: false,
            };

        case SET_MY_REPAIR_NOTES_TOTAL:
            return {
                ...state,
                myRepairNotesTotal: action.payload,
            };
            
        case GET_REPAIR_NOTES_IN_WORK_STARTED:
            return {
                ...state,
                isRepairNotesLoading: true,
            };

        case GET_REPAIR_NOTES_IN_WORK_SUCCESS:
            return {
                ...state,
                repairNotesInWork: action.payload.page === 1
                    ? action.payload.data
                    : [...state.repairNotesInWork, ...action.payload.data],
                isRepairNotesLoading: false,
            };

        case GET_REPAIR_NOTES_IN_WORK_FAILED:
            return {
                ...state,
                isRepairNotesLoading: false,
            };

        case SET_REPAIR_NOTES_IN_WORK_TOTAL:
            return {
                ...state,
                repairNotesInWorkTotal: action.payload,
            };

        default:
            return state;
    }
};
