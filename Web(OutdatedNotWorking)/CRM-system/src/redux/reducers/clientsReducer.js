import {
    GET_CLIENTS_FAILED,
    GET_CLIENTS_STARTED,
    GET_CLIENTS_SUCCESS,
    GETMY_CLIENT_FAILED,
    GETMY_CLIENT_STARTED,
    GETMY_CLIENT_SUCCESS,
    POST_CLIENT_FAILED,
    POST_CLIENT_STARTED,
    POST_CLIENT_SUCCESS,
    PUT_CLIENT_FAILED,
    PUT_CLIENT_STARTED,
    PUT_CLIENT_SUCCESS,
    SET_CLIENTS_TOTAL
} from "../actionCreators/clients";

const initialeState = {
    clients: [],
    myClient: [],
    isClientsLoading: true,
    totalClients: 0,
};

export const clientsReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_CLIENTS_STARTED:
            return {
                ...state,
                isClientsLoading: true,
            };
        case GET_CLIENTS_SUCCESS:
            return {
                ...state,
                clients:
                    action.payload.page === 1
                        ? action.payload.data
                        : [...state.clients, ...action.payload.data],
                isClientsLoading: false,
            };
        case GET_CLIENTS_FAILED:
            return {
                ...state,
                isClientsLoading: false,
            };
        case SET_CLIENTS_TOTAL:
            return {
                ...state,
                totalClients: action.payload,
            };

        case GETMY_CLIENT_STARTED:
            return {
                ...state,
                isClientsLoading: true,
            };
        case GETMY_CLIENT_SUCCESS:
            return {
                ...state,
                myClient: action.payload,
                isClientsLoading: false,
            };
        case GETMY_CLIENT_FAILED:
            return {
                ...state,
                isClientsLoading: false,
            };

        case POST_CLIENT_STARTED:
            return {
                ...state,
                isClientsLoading: true,
            };
        case POST_CLIENT_SUCCESS:
            return {
                ...state,
                clients: [action.payload, ...state.clients],
                isClientsLoading: false,
            };
        case POST_CLIENT_FAILED:
            return {
                ...state,
                isClientsLoading: false,
            };

        case PUT_CLIENT_STARTED:
            return {
                ...state,
                isClientsLoading: true,
            };
        case PUT_CLIENT_SUCCESS:
            return {
                ...state,
                clients: state.clients.map(item => item.id === action.payload.id ? action.payload : item),
                isClientsLoading: false,
            };
        case PUT_CLIENT_FAILED:
            return {
                ...state,
                isClientsLoading: false,
            };
        default:
            return {
                ...state,
            };
    }
}