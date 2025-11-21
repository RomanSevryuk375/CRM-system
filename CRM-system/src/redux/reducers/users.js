import { CREATE_USER_FAILED, CREATE_USER_STARTED, CREATE_USER_SUCCESS, DELETE_USER_FAILED, DELETE_USER_STARTED, DELETE_USER_SUCCESS, LOGIN_USER_FAILED, LOGIN_USER_STARTED, LOGIN_USER_SUCCESS, LOGOUT_USER_FAILED, LOGOUT_USER_STARTED, LOGOUT_USER_SUCCESS } from "../actionCreators/users";
const token = localStorage.getItem("jwt");

const initialeState = {
    users: [],
    isLoggedIn: !!token,
    isUsersLoading: true,
};

export const usersReducer = (state = initialeState, action) => {
    switch (action.type) {
        case LOGIN_USER_STARTED:
            return {
                ...state,
                isUsersLoading: true,
            };
        case LOGIN_USER_SUCCESS:
            return {
                ...state,
                isLoggedIn: true,
                isUsersLoading: false,
            };
        case LOGIN_USER_FAILED:
            return {
                ...state,
                isUsersLoading: false,
            };
             
        case LOGOUT_USER_STARTED:
            return {
                ...state,
                isUsersLoading: true,
            };
        case LOGOUT_USER_SUCCESS:
            return {
                ...state,
                isLoggedIn: false,
                isUsersLoading: false,
            };
        case LOGOUT_USER_FAILED: 
            return {
                ...state,
                isUsersLoading: false,
            };
        
        case CREATE_USER_STARTED:
            return {
                ...state,
                isUsersLoading: true,
            };
        case CREATE_USER_SUCCESS:
            return {
                ...state,
                isUsersLoading: false,
                isLoggedIn: true,
                users: [action.payload, ...state.users],
            };
        case CREATE_USER_FAILED:
            return {
                ...state,
                isUsersLoading: false,
            };
    
        case DELETE_USER_STARTED:
            return { 
                ...state,
                isUsersLoading: true,
            };
        case DELETE_USER_SUCCESS:
            return {
                ...state,
                isUsersLoading: false,
                users: state.users.filter(item => item.id != action.payload),
            };
        case DELETE_USER_FAILED:
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