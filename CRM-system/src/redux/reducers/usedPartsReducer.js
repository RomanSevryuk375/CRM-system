import {
    GET_USED_PARTS_WITH_INFO_STARTED,
    GET_USED_PARTS_WITH_INFO_SUCCESS,
    GET_USED_PARTS_WITH_INFO_FAILED,
    SET_USED_PARTS_WITH_INFO_TOTAL,
    GET_USED_PARTS_IN_WORK_STARTED,
    GET_USED_PARTS_IN_WORK_SUCCESS,
    GET_USED_PARTS_IN_WORK_FAILED,
    SET_USED_PARTS_IN_WORK_TOTAL,
    POST_USED_PART_STARTED,
    POST_USED_PART_SUCCESS,
    POST_USED_PART_FAILED,
    PUT_USED_PART_STARTED,
    PUT_USED_PART_SUCCESS,
    PUT_USED_PART_FAILED,
    DELETE_USED_PART_STARTED,
    DELETE_USED_PART_SUCCESS,
    DELETE_USED_PART_FAILED
} from "../actionCreators/usedParts";

const initialState = {
    usedPartsWithInfo: [],
    usedPartsInWork: [],
    usedPartsWithInfoTotal: 0,
    usedPartsInWorkTotal: 0,

    isUsedPartsWithInfoLoading: true,
    isUsedPartsInWorkLoading: true,

    isCreateUsedPartLoading: false,
    isUpdateUsedPartLoading: false,
    isDeleteUsedPartLoading: false,
};

export const usedPartsReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_USED_PARTS_WITH_INFO_STARTED:
            return {
                ...state,
                isUsedPartsWithInfoLoading: true,
            };

        case GET_USED_PARTS_WITH_INFO_SUCCESS:
            return {
                ...state,
                usedPartsWithInfo: action.payload.page === 1
                    ? action.payload.data
                    : [...state.usedPartsInWorkTotal, ...action.payload.data],
                isUsedPartsWithInfoLoading: false,
            };

        case GET_USED_PARTS_WITH_INFO_FAILED:
            return {
                ...state,
                isUsedPartsWithInfoLoading: false,
            };

        case SET_USED_PARTS_WITH_INFO_TOTAL:
            return {
                ...state,
                usedPartsWithInfoTotal: action.payload,
            };

        case GET_USED_PARTS_IN_WORK_STARTED:
            return {
                ...state,
                isUsedPartsInWorkLoading: true,
            };

        case GET_USED_PARTS_IN_WORK_SUCCESS:
            return {
                ...state,
                usedPartsInWork: action.payload.page === 1
                    ? action.payload.data
                    : [...state.usedPartsInWork, ...action.payload.data],
                isUsedPartsInWorkLoading: false,
            };

        case GET_USED_PARTS_IN_WORK_FAILED:
            return {
                ...state,
                isUsedPartsInWorkLoading: false,
            };

        case SET_USED_PARTS_IN_WORK_TOTAL:
            return {
                ...state,
                usedPartsInWorkTotal: action.payload,
            };

        case POST_USED_PART_STARTED:
            return {
                ...state,
                isCreateUsedPartLoading: true,
            };

        case POST_USED_PART_SUCCESS:
            return {
                ...state,
                usedPartsWithInfo: [...state.usedPartsWithInfo, action.payload],
                isCreateUsedPartLoading: false,
            };

        case POST_USED_PART_FAILED:
            return {
                ...state,
                isCreateUsedPartLoading: false,
            };

        case PUT_USED_PART_STARTED:
            return {
                ...state,
                isUpdateUsedPartLoading: true,
            };

        case PUT_USED_PART_SUCCESS:
            return {
                ...state,
                usedPartsWithInfo: state.usedPartsWithInfo.map((u) =>
                    u.id === action.payload.id ? action.payload : u
                ),
                isUpdateUsedPartLoading: false,
            };

        case PUT_USED_PART_FAILED:
            return {
                ...state,
                isUpdateUsedPartLoading: false,
            };

        case DELETE_USED_PART_STARTED:
            return {
                ...state,
                isDeleteUsedPartLoading: true,
            };

        case DELETE_USED_PART_SUCCESS:
            return {
                ...state,
                usedPartsWithInfo: state.usedPartsWithInfo.filter((u) => u.id !== action.payload),
                isDeleteUsedPartLoading: false,
            };

        case DELETE_USED_PART_FAILED:
            return {
                ...state,
                isDeleteUsedPartLoading: false,
            };

        default:
            return state;
    }
};
