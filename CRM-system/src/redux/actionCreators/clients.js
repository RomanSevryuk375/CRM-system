export const GET_CLIENTS_STARTED = "GET_CLIENTS_STARTED";
export const GET_CLIENTS_SUCCESS = "GET_CLIENTS_SUCCESS";
export const GET_CLIENTS_FAILED = "GET_CLIENTS_FAILED";
export const SET_CLIENTS_TOTAL = "SET_CLIENTS_TOTAL";

export const GETMY_CLIENT_STARTED = "GETMY_CLIENT_STARTED";
export const GETMY_CLIENT_SUCCESS = "GETMY_CLIENT_SUCCESS";
export const GETMY_CLIENT_FAILED = "GETMY_CLIENT_FAILED";

export const POST_CLIENT_STARTED = "POST_CLIENT_STARTED";
export const POST_CLIENT_SUCCESS = "POST_CLIENT_SUCCESS";
export const POST_CLIENT_FAILED = "POST_CLIENT_FAILED";

export const PUT_CLIENT_STARTED = "PUT_CLIENT_STARTED";
export const PUT_CLIENT_SUCCESS = "PUT_CLIENT_SUCCESS";
export const PUT_CLIENT_FAILED = "PUT_CLIENT_FAILED";

export const getClientsStarted = () => ({
    type: GET_CLIENTS_STARTED,
});
export const getClientsSuccess = (clients) => ({
    type: GET_CLIENTS_SUCCESS,
    payload: clients,
});
export const getClientsFailed = (error) => ({
    type: GET_CLIENTS_FAILED,
    payload: error,
});
export const setClientsTotal = (total) => ({
    type: SET_CLIENTS_TOTAL,
    payload: total,
});

export const getMyClientStarted = () => ({
    type: GETMY_CLIENT_STARTED,
});
export const getMyClientSuccess = (client) => ({
    type: GETMY_CLIENT_SUCCESS,
    payload: client,
});
export const getMyClientFailed = (error) => ({
    type: GETMY_CLIENT_FAILED,
    payload: error,
});

export const createClientStarted = () => ({
    type: POST_CLIENT_STARTED,
});
export const createClientSuccess = (client) => ({
    type: POST_CLIENT_SUCCESS,
    payload: client,
});
export const createClientFailed = (error) => ({
    type: POST_CLIENT_FAILED,
    payload: error,
});

export const updateClientStarted = () => ({
    type: PUT_CLIENT_STARTED,
});
export const updateClientSuccess = (client) => ({
    type: PUT_CLIENT_SUCCESS,
    payload: client,
});
export const updateClientFailed = (error) => ({
    type: PUT_CLIENT_FAILED,
    payload: error,
});

