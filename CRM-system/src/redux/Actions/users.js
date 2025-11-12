import { api } from "../../api";
import { getUsersFailed, getUsersStarted, getUsersSuccess } from "../actionCreators/users";

export const getUsers = () => {
    return async (dispatch) => {
        try {
            dispatch(getUsersStarted);
            const response = await api.users.getUsers({
                // params: {
                //     _page: 0,
                //     _limit: 1,
                // },
            });
            dispatch(getUsersSuccess(response.data));
        } catch (error) {
            dispatch(getUsersFailed(error));            
        }
    };
};