import { GET_USERS_FAILED, GET_USERS_STARTED, GET_USERS_SUCCESS } from "../actionCreators/users";

const initialeState = {
    users: [],
    isUsersLoading: true,
}

export const usersReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_USERS_STARTED:
            return {
                ...state,
                isUsersLoading: true,
            };

        case GET_USERS_SUCCESS:
            return {
                ...state,
                users: action.payload,
                isUsersLoading: false,
            };

        case GET_USERS_FAILED:
            return {
                ...state,
                isUsersLoading: false,
            };
    
        default:
            return {
                ...state,
            };
    };
};