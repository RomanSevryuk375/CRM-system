import {
    GET_WORKS_WITH_INFO_STARTED,
    GET_WORKS_WITH_INFO_SUCCESS,
    GET_WORKS_WITH_INFO_FAILED,
    SET_WORKS_WITH_INFO_TOTAL,

    GET_MY_WORKS_STARTED,
    GET_MY_WORKS_SUCCESS,
    GET_MY_WORKS_FAILED,
    SET_MY_WORKS_TOTAL,

    POST_WORK_STARTED,
    POST_WORK_SUCCESS,
    POST_WORK_FAILED,

    PUT_WORK_STARTED,
    PUT_WORK_SUCCESS,
    PUT_WORK_FAILED,

    DELETE_WORK_STARTED,
    DELETE_WORK_SUCCESS,
    DELETE_WORK_FAILED,
} from "../actionCreators/works";

const initialState = {
    worksWithInfo: [],
    worksWithInfoTotal: 0,

    myWorks: [],
    myWorksTotal: 0,

    loading: false,
    error: null,
};

export default function worksReducer(state = initialState, action) {
    switch (action.type) {

        case GET_WORKS_WITH_INFO_STARTED:
            return { ...state, loading: true, error: null };

        case GET_WORKS_WITH_INFO_SUCCESS:
            return {
                ...state,
                loading: false,
                worksWithInfo: action.payload,
            };

        case GET_WORKS_WITH_INFO_FAILED:
            return { ...state, loading: false, error: action.payload };

        case SET_WORKS_WITH_INFO_TOTAL:
            return { ...state, worksWithInfoTotal: action.payload };

        case GET_MY_WORKS_STARTED:
            return { ...state, loading: true, error: null };

        case GET_MY_WORKS_SUCCESS:
            return {
                ...state,
                loading: false,
                myWorks: action.payload,
            };

        case GET_MY_WORKS_FAILED:
            return { ...state, loading: false, error: action.payload };

        case SET_MY_WORKS_TOTAL:
            return { ...state, myWorksTotal: action.payload };

        case POST_WORK_STARTED:
            return { ...state, loading: true, error: null };

        case POST_WORK_SUCCESS:
            return {
                ...state,
                loading: false,
                worksWithInfo: [action.payload, ...state.worksWithInfo],
            };

        case POST_WORK_FAILED:
            return { ...state, loading: false, error: action.payload };

        case PUT_WORK_STARTED:
            return { ...state, loading: true, error: null };

        case PUT_WORK_SUCCESS:
            return {
                ...state,
                loading: false,
                worksWithInfo: state.worksWithInfo.map((w) =>
                    w.id === action.payload.id ? action.payload : w
                ),
            };

        case PUT_WORK_FAILED:
            return { ...state, loading: false, error: action.payload };

        case DELETE_WORK_STARTED:
            return { ...state, loading: true, error: null };

        case DELETE_WORK_SUCCESS:
            return {
                ...state,
                loading: false,
                worksWithInfo: state.worksWithInfo.filter(
                    (w) => w.id !== action.payload
                ),
            };

        case DELETE_WORK_FAILED:
            return { ...state, loading: false, error: action.payload };

        default:
            return state;
    }
}
