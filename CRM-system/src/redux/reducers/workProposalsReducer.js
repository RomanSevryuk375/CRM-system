import {
    GET_WORK_PROPOSAL_WITH_INFO_STARTED,
    GET_WORK_PROPOSAL_WITH_INFO_SUCCESS,
    GET_WORK_PROPOSAL_WITH_INFO_FAILED,
    SET_WORK_PROPOSAL_WITH_INFO_TOTAL,
    GET_WORK_PROPOSAL_IN_WORK_STARTED,
    GET_WORK_PROPOSAL_IN_WORK_SUCCESS,
    GET_WORK_PROPOSAL_IN_WORK_FAILED,
    SET_WORK_PROPOSAL_IN_WORK_TOTAL,
    POST_WORK_PROPOSAL_STARTED,
    POST_WORK_PROPOSAL_SUCCESS,
    POST_WORK_PROPOSAL_FAILED,
    PUT_WORK_PROPOSAL_STARTED,
    PUT_WORK_PROPOSAL_SUCCESS,
    PUT_WORK_PROPOSAL_FAILED,
    PUT_WORK_PROPOSAL_ACCEPT_STARTED,
    PUT_WORK_PROPOSAL_ACCEPT_SUCCESS,
    PUT_WORK_PROPOSAL_ACCEPT_FAILED,
    PUT_WORK_PROPOSAL_REJECT_STARTED,
    PUT_WORK_PROPOSAL_REJECT_SUCCESS,
    PUT_WORK_PROPOSAL_REJECT_FAILED,
    DELETE_WORK_PROPOSAL_STARTED,
    DELETE_WORK_PROPOSAL_SUCCESS,
    DELETE_WORK_PROPOSAL_FAILED,
} from "../actionCreators/workProposals";


const initialState = {
    workProposalsWithInfo: [],
    workProposalsWithInfoTotal: 0,

    workProposalsInWork: [],
    workProposalsInWorkTotal: 0,

    loading: false,
    error: null,
};

export default function workProposalsReducer(state = initialState, action) {
    switch (action.type) {
        
        case GET_WORK_PROPOSAL_WITH_INFO_STARTED:
            return { ...state, loading: true, error: null };

        case GET_WORK_PROPOSAL_WITH_INFO_SUCCESS:
            return {
                ...state,
                loading: false,
                workProposalsWithInfo: action.payload,
            };

        case GET_WORK_PROPOSAL_WITH_INFO_FAILED:
            return { ...state, loading: false, error: action.payload };

        case SET_WORK_PROPOSAL_WITH_INFO_TOTAL:
            return { ...state, workProposalsWithInfoTotal: action.payload };

        case GET_WORK_PROPOSAL_IN_WORK_STARTED:
            return { ...state, loading: true, error: null };

        case GET_WORK_PROPOSAL_IN_WORK_SUCCESS:
            return {
                ...state,
                loading: false,
                workProposalsInWork: action.payload,
            };

        case GET_WORK_PROPOSAL_IN_WORK_FAILED:
            return { ...state, loading: false, error: action.payload };

        case SET_WORK_PROPOSAL_IN_WORK_TOTAL:
            return { ...state, workProposalsInWorkTotal: action.payload };

        case POST_WORK_PROPOSAL_STARTED:
            return { ...state, loading: true, error: null };

        case POST_WORK_PROPOSAL_SUCCESS:
            return {
                ...state,
                loading: false,
                workProposalsWithInfo: [
                    action.payload,
                    ...state.workProposalsWithInfo,
                ],
            };

        case POST_WORK_PROPOSAL_FAILED:
            return { ...state, loading: false, error: action.payload };

        case PUT_WORK_PROPOSAL_STARTED:
        case PUT_WORK_PROPOSAL_ACCEPT_STARTED:
        case PUT_WORK_PROPOSAL_REJECT_STARTED:
            return { ...state, loading: true, error: null };

        case PUT_WORK_PROPOSAL_SUCCESS:
        case PUT_WORK_PROPOSAL_ACCEPT_SUCCESS:
        case PUT_WORK_PROPOSAL_REJECT_SUCCESS:
            return {
                ...state,
                loading: false,
                workProposalsWithInfo: state.workProposalsWithInfo.map((wp) =>
                    wp.id === action.payload.id ? action.payload : wp
                ),
            };

        case PUT_WORK_PROPOSAL_FAILED:
        case PUT_WORK_PROPOSAL_ACCEPT_FAILED:
        case PUT_WORK_PROPOSAL_REJECT_FAILED:
            return { ...state, loading: false, error: action.payload };

        case DELETE_WORK_PROPOSAL_STARTED:
            return { ...state, loading: true, error: null };

        case DELETE_WORK_PROPOSAL_SUCCESS:
            return {
                ...state,
                loading: false,
                workProposalsWithInfo: state.workProposalsWithInfo.filter(
                    (wp) => wp.id !== action.payload
                ),
            };

        case DELETE_WORK_PROPOSAL_FAILED:
            return { ...state, loading: false, error: action.payload };

        default:
            return state;
    }
}
