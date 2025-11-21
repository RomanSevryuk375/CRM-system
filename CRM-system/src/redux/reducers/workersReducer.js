import {
    GET_WORKER_WITH_INFO_STARTED,
    GET_WORKER_WITH_INFO_SUCCESS,
    GET_WORKER_WITH_INFO_FAILED,
    SET_WORKER_WITH_INFO_TOTAL,
    GET_MY_WORKER_STARTED,
    GET_MY_WORKER_SUCCESS,
    GET_MY_WORKER_FAILED,
    POST_WORKER_STARTED,
    POST_WORKER_SUCCESS,
    POST_WORKER_FAILED,
    PUT_WORKER_STARTED,
    PUT_WORKER_SUCCESS,
    PUT_WORKER_FAILED,
    DELETE_WORKER_STARTED,
    DELETE_WORKER_SUCCESS,
    DELETE_WORKER_FAILED
} from "../actionCreators/workers";

const initialState = {
    workersWithInfo: [],
    workersWithInfoTotal: 0,

    myWorker: null,

    isWorkersWithInfoLoading: true,
    isMyWorkerLoading: true,

    isCreateWorkerLoading: false,
    isUpdateWorkerLoading: false,
    isDeleteWorkerLoading: false,
};

export const workersReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_WORKER_WITH_INFO_STARTED:
            return {
                ...state,
                isWorkersWithInfoLoading: true,
            };

        case GET_WORKER_WITH_INFO_SUCCESS:
            return {
                ...state,
                workersWithInfo: action.payload.page === 1
                    ? action.payload.data
                    : [...state.workersWithInfo, ...action.payload.data],
                isWorkersWithInfoLoading: false,
            };

        case GET_WORKER_WITH_INFO_FAILED:
            return {
                ...state,
                isWorkersWithInfoLoading: false,
            };

        case SET_WORKER_WITH_INFO_TOTAL:
            return {
                ...state,
                workersWithInfoTotal: action.payload,
            };

        case GET_MY_WORKER_STARTED:
            return {
                ...state,
                isMyWorkerLoading: true,
            };

        case GET_MY_WORKER_SUCCESS:
            return {
                ...state,
                myWorker: action.payload,
                isMyWorkerLoading: false,
            };

        case GET_MY_WORKER_FAILED:
            return {
                ...state,
                isMyWorkerLoading: false,
            };

        case POST_WORKER_STARTED:
            return {
                ...state,
                isCreateWorkerLoading: true,
            };

        case POST_WORKER_SUCCESS:
            return {
                ...state,
                workersWithInfo: [...state.workersWithInfo, action.payload],
                isCreateWorkerLoading: false,
            };

        case POST_WORKER_FAILED:
            return {
                ...state,
                isCreateWorkerLoading: false,
            };

        case PUT_WORKER_STARTED:
            return {
                ...state,
                isUpdateWorkerLoading: true,
            };

        case PUT_WORKER_SUCCESS:
            return {
                ...state,
                workersWithInfo: state.workersWithInfo.map(w =>
                    w.id === action.payload.id ? action.payload : w
                ),
                isUpdateWorkerLoading: false,
            };

        case PUT_WORKER_FAILED:
            return {
                ...state,
                isUpdateWorkerLoading: false,
            };

        case DELETE_WORKER_STARTED:
            return {
                ...state,
                isDeleteWorkerLoading: true,
            };

        case DELETE_WORKER_SUCCESS:
            return {
                ...state,
                workersWithInfo: state.workersWithInfo.filter(w => w.id !== action.payload),
                isDeleteWorkerLoading: false,
            };

        case DELETE_WORKER_FAILED:
            return {
                ...state,
                isDeleteWorkerLoading: false,
            };

        default:
            return state;
    }
};
