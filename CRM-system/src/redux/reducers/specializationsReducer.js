import {GET_SPECIALIZATIONS_STARTED, GET_SPECIALIZATIONS_SUCCESS, GET_SPECIALIZATIONS_FAILED, SET_SPECIALIZATIONS_TOTAL, POST_SPECIALIZATION_STARTED, POST_SPECIALIZATION_SUCCESS, POST_SPECIALIZATION_FAILED, PUT_SPECIALIZATION_STARTED, PUT_SPECIALIZATION_SUCCESS, PUT_SPECIALIZATION_FAILED, DELETE_SPECIALIZATION_STARTED, DELETE_SPECIALIZATION_SUCCESS, DELETE_SPECIALIZATION_FAILED
} from "../actionCreators/specializations";

const initialState = {
    specializations: [],
    specializationsTotal: 0,
    isSpecializationsLoading: true,
};

export const specializationsReducer = (state = initialState, action) => {
    switch (action.type) {

        case GET_SPECIALIZATIONS_STARTED:
            return {
                ...state,
                isSpecializationsLoading: true,
            };

        case GET_SPECIALIZATIONS_SUCCESS:
            return {
                ...state,
                specializations: action.payload.page === 1
                    ? action.payload.data
                    : [...state.specializations, ...action.payload.data],
                isSpecializationsLoading: false,
            };

        case GET_SPECIALIZATIONS_FAILED:
            return {
                ...state,
                isSpecializationsLoading: false,
            };

        case SET_SPECIALIZATIONS_TOTAL:
            return {
                ...state,
                specializationsTotal: action.payload,
            };

        case POST_SPECIALIZATION_STARTED:
            return {
                ...state,
                isSpecializationsLoading: true,
            };

        case POST_SPECIALIZATION_SUCCESS:
            return {
                ...state,
                specializations: [action.payload, ...state.specializations],
                isSpecializationsLoading: false,
            };

        case POST_SPECIALIZATION_FAILED:
            return {
                ...state,
                isSpecializationsLoading: false,
            };

        case PUT_SPECIALIZATION_STARTED:
            return {
                ...state,
                isSpecializationsLoading: true,
            };

        case PUT_SPECIALIZATION_SUCCESS:
            return {
                ...state,
                specializations: state.specializations.map(item =>
                    item.id === action.payload.id ? action.payload : item
                ),
                isSpecializationsLoading: false,
            };

        case PUT_SPECIALIZATION_FAILED:
            return {
                ...state,
                isSpecializationsLoading: false,
            };

        case DELETE_SPECIALIZATION_STARTED:
            return {
                ...state,
                isSpecializationsLoading: true,
            };

        case DELETE_SPECIALIZATION_SUCCESS:
            return {
                ...state,
                specializations: state.specializations.filter(item => item.id !== action.payload),
                isSpecializationsLoading: false,
            };

        case DELETE_SPECIALIZATION_FAILED:
            return {
                ...state,
                isSpecializationsLoading: false,
            };

        default:
            return state;
    }
};
