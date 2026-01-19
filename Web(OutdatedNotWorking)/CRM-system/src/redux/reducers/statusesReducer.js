import {GET_STATUSES_STARTED, GET_STATUSES_SUCCESS, GET_STATUSES_FAILED} from "../actionCreators/statuses";

const initialState = {
    statuses: [],
    isStatusesLoading: true,
};

export const statusesReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_STATUSES_STARTED:
            return {
                ...state,
                isStatusesLoading: true,
            };

        case GET_STATUSES_SUCCESS:
            return {
                ...state,
                statuses: action.payload,
                isStatusesLoading: false,
            };

        case GET_STATUSES_FAILED:
            return {
                ...state,
                isStatusesLoading: false,
            };

        default:
            return state;
    }
};
