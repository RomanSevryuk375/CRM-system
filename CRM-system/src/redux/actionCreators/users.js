export const LOGIN_USER_STARTED = "LOGIN_USER_STARTED";
export const LOGIN_USER_SUCCESS = "LOGIN_USER_SUCCESS";
export const LOGIN_USER_FAILED = "LOGIN_USER_FAILED";

export const LOGOUT_USER_STARTED = "LOGOUT_USER_STARTED";
export const LOGOUT_USER_SUCCESS = "LOGOUT_USER_SUCCESS";
export const LOGOUT_USER_FAILED = "LOGOUT_USER_FAILED";

export const CREATE_USER_STARTED = "CREATE_USER_STARTED";
export const CREATE_USER_SUCCESS = "CREATE_USER_SUCCESS";
export const CREATE_USER_FAILED = "CREATE_USER_FAILED";

export const DELETE_USER_STARTED = "DELETE_USER_STARTED";
export const DELETE_USER_SUCCESS = "DELETE_USER_SUCCESS";
export const DELETE_USER_FAILED = "DELETE_USER_FAILED";

export const loginUserStarted = () => ({
    type: LOGIN_USER_STARTED,
});
export const loginUserSuccess = (data) => ({
    type: LOGIN_USER_SUCCESS,
    payload: data,
});
export const loginUserFailed = (error) => ({
    type: LOGIN_USER_FAILED,
    payload: error,
});

export const logoutUserStarted = () => ({
    type: LOGOUT_USER_STARTED,
});
export const logoutUserSuccess = () => ({
    type: LOGOUT_USER_SUCCESS,
});
export const logoutUserFailed = (error) => ({
    type: LOGOUT_USER_FAILED,
    payload: error,
});

export const createUserStarted = () => ({
    type: CREATE_USER_STARTED,
});
export const createUserSuccess = (user) => ({
    type: CREATE_USER_SUCCESS,
    payload: user,
});
export const createUserFailed = (error) => ({
    type: CREATE_USER_FAILED,
    payload: error,
});

export const deleteUserStarted = () => ({
    type: DELETE_USER_STARTED,
});
export const deleteUserSuccess = (id) => ({
    type: DELETE_USER_SUCCESS,
    payload: id,
});
export const deleteUserFailed = (error) => ({
    type: DELETE_USER_FAILED,
    payload: error,
});