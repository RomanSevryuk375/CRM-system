import { api } from "../../api";
import { createClientFailed, createClientStarted, createClientSuccess, getClientsFailed, getClientsStarted, getClientsSuccess, getMyClientFailed, getMyClientStarted, getMyClientSuccess, setClientsTotal, updateClientFailed, updateClientStarted, updateClientSuccess } from "../actionCreators/clients"

export const getClients = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getClientsStarted());

            const response = await api.clients.getClients({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers['x-total-count'], 10);
            if (!isNaN(totalCount)) {
                dispatch(setClientsTotal(totalCount));
            }

            dispatch(getClientsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getClientsFailed(error));
        }
    };
};

export const getMyClient = () => {
    return async (dispatch) => {
        try {
            dispatch(getMyClientStarted());

            const response = await api.clients.getMyClient({});

            dispatch(getMyClientSuccess(response.data));
        } catch (error) {
            dispatch(getMyClientFailed(error));
        }
    };
};

export const createClient = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createClientStarted());

            const response = await api.clients.createClient(data);

            dispatch(createClientSuccess(response.data));
        } catch (error) {
            dispatch(createClientFailed(error));
        }
    };
};

export const updateClient = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateClientStarted());

            const response = await api.clients.updateClient(id, data);

            dispatch(updateClientSuccess(response.data));
        } catch (error) {
            dispatch(updateClientFailed(error));            
        }
    };
};