import { api } from "../../api"
import { getStatusesFailed, getStatusesStarted, getStatusesSuccess } from "../actionCreators/statuses"

export const getStatuses = () => {
    return async (dispatch) => {
        try {
            dispatch(getStatusesStarted())

            const response = await api.statuses.getStatuses();

            dispatch(getStatusesSuccess(response.data));
        } catch (error) {
            dispatch(getStatusesFailed(error));
        }
    };
};