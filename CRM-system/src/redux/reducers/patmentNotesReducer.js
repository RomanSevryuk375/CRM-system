import {GET_PAYMENT_NOTES_STARTED, GET_PAYMENT_NOTES_SUCCESS, GET_PAYMENT_NOTES_FAILED, SET_PAYMENT_NOTES_TOTAL, GET_MY_PAYMENT_NOTES_STARTED, GET_MY_PAYMENT_NOTES_SUCCESS, GET_MY_PAYMENT_NOTES_FAILED, SET_MY_PAYMENT_NOTES_TOTAL, POST_PAYMENT_NOTE_STARTED, POST_PAYMENT_NOTE_SUCCESS, POST_PAYMENT_NOTE_FAILED, PUT_PAYMENT_NOTE_STARTED, PUT_PAYMENT_NOTE_SUCCESS, PUT_PAYMENT_NOTE_FAILED, DELETE_PAYMENT_NOTE_STARTED, DELETE_PAYMENT_NOTE_SUCCESS, DELETE_PAYMENT_NOTE_FAILED} from "../actionCreators/paymentNotes";

const initialeState = {
    paymentNotes: [],
    myPaymentNotes: [],
    paymentNotesTotal: 0,
    myPaymentNotesTotal: 0,
    isPaymentNotesLoading: true,
};

export const paymentNotesReducer = (state = initialeState, action) => {
    switch (action.type) {

        case GET_PAYMENT_NOTES_STARTED:
            return {
                ...state,
                isPaymentNotesLoading: true,
            };

        case GET_PAYMENT_NOTES_SUCCESS:
            return {
                ...state,
                paymentNotes: action.payload.page === 1
                    ? action.payload.data
                    : [...state.paymentNotes, ...action.payload.data],
                isPaymentNotesLoading: false,
            };

        case GET_PAYMENT_NOTES_FAILED:
            return {
                ...state,
                isPaymentNotesLoading: false,
            };

        case SET_PAYMENT_NOTES_TOTAL:
            return {
                ...state,
                paymentNotesTotal: action.payload,
            };

        case GET_MY_PAYMENT_NOTES_STARTED:
            return {
                ...state,
                isPaymentNotesLoading: true,
            };

        case GET_MY_PAYMENT_NOTES_SUCCESS:
            return {
                ...state,
                myPaymentNotes: action.payload.page === 1
                    ? action.payload.data
                    : [...state.myPaymentNotes, ...action.payload.data],
                isPaymentNotesLoading: false,
            };

        case GET_MY_PAYMENT_NOTES_FAILED:
            return {
                ...state,
                isPaymentNotesLoading: false,
            };

        case SET_MY_PAYMENT_NOTES_TOTAL:
            return {
                ...state,
                myPaymentNotesTotal: action.payload,
            };

        case POST_PAYMENT_NOTE_STARTED:
            return {
                ...state,
                isPaymentNotesLoading: true,
            };

        case POST_PAYMENT_NOTE_SUCCESS:
            return {
                ...state,
                paymentNotes: [action.payload, ...state.paymentNotes],
                isPaymentNotesLoading: false,
            };

        case POST_PAYMENT_NOTE_FAILED:
            return {
                ...state,
                isPaymentNotesLoading: false,
            };

        case PUT_PAYMENT_NOTE_STARTED:
            return {
                ...state,
                isPaymentNotesLoading: true,
            };

        case PUT_PAYMENT_NOTE_SUCCESS:
            return {
                ...state,
                paymentNotes: state.paymentNotes.map(item =>
                    item.id === action.payload.id ? action.payload : item
                ),
                isPaymentNotesLoading: false,
            };

        case PUT_PAYMENT_NOTE_FAILED:
            return {
                ...state,
                isPaymentNotesLoading: false,
            };

        case DELETE_PAYMENT_NOTE_STARTED:
            return {
                ...state,
                isPaymentNotesLoading: true,
            };

        case DELETE_PAYMENT_NOTE_SUCCESS:
            return {
                ...state,
                paymentNotes: state.paymentNotes.filter(item => item.id !== action.payload),
                isPaymentNotesLoading: false,
            };

        case DELETE_PAYMENT_NOTE_FAILED:
            return {
                ...state,
                isPaymentNotesLoading: false,
            };

        default:
            return state;
    }
};
