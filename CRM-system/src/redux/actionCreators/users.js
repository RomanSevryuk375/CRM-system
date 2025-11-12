export const GET_USERS_STARTED = "GET_USERS_STARTED";
export const GET_USERS_SUCCESS = "GET_USERS_SUCCESS";
export const GET_USERS_FAILED = "GET_USERS_FAILED";

export const getUsersStarted = () => ({
    type: GET_USERS_STARTED,
});

export const getUsersSuccess = (users) => ({
    type: GET_USERS_SUCCESS,
    payload: users,
});

export const getUsersFailed = (error) => ({
    type: GET_USERS_FAILED,
    payload: error,
});