import { api } from "../../api";
import { createUserFailed, createUserStarted, createUserSuccess, deleteUserFailed, deleteUserStarted, deleteUserSuccess, loginUserFailed, loginUserStarted, loginUserSuccess, logoutUserStarted, logoutUserSuccess } from "../actionCreators/users";


export const loginUser = (login, password) => {
    return async (dispatch) => {
        try {
            dispatch(loginUserStarted());

            const response = await api.users.loginUser({ data: { login, password } });
            
            if (response.data.token) {
                localStorage.setItem("jwt", response.data.token);
            }

            dispatch(loginUserSuccess(response.data));
        } catch (error) {
            dispatch(loginUserFailed(error));
        }
    };
};

export const logoutUser = () => {
    return async (dispatch) => {
        dispatch(logoutUserStarted());

        try {
            await api.users.logoutUser();
            localStorage.removeItem("jwt");

            dispatch(logoutUserSuccess());
        } catch (error) {
            dispatch(logoutUserSuccess(error));
        }
    };
};

export const createUser = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createUserStarted());

            const response = await api.users.createUser({data});

            dispatch(createUserSuccess(response.data));
        } catch (error) {
            dispatch(createUserFailed(error));
        }
    };
};

export const deleteUser = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteUserStarted());

            const response = await api.users.deleteUser(id, {});

            dispatch(deleteUserSuccess(response.data));
        }
        catch (error) {
            dispatch(deleteUserFailed(error));
        }
    };
};