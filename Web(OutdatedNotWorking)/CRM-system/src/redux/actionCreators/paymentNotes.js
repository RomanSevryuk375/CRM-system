export const GET_PAYMENT_NOTES_STARTED = "GET_PAYMENT_NOTES_STARTED";
export const GET_PAYMENT_NOTES_SUCCESS = "GET_PAYMENT_NOTES_SUCCESS";
export const GET_PAYMENT_NOTES_FAILED = "GET_PAYMENT_NOTES_FAILED";
export const SET_PAYMENT_NOTES_TOTAL = "SET_PAYMENT_NOTES_TOTAL";

export const GET_MY_PAYMENT_NOTES_STARTED = "GET_MY_PAYMENT_NOTES_STARTED";
export const GET_MY_PAYMENT_NOTES_SUCCESS = "GET_MY_PAYMENT_NOTES_SUCCESS";
export const GET_MY_PAYMENT_NOTES_FAILED = "GET_MY_PAYMENT_NOTES_FAILED";
export const SET_MY_PAYMENT_NOTES_TOTAL = "SET_MY_PAYMENT_NOTES_TOTAL";

export const POST_PAYMENT_NOTE_STARTED = "POST_PAYMENT_NOTE_STARTED";
export const POST_PAYMENT_NOTE_SUCCESS = "POST_PAYMENT_NOTE_SUCCESS";
export const POST_PAYMENT_NOTE_FAILED = "POST_PAYMENT_NOTE_FAILED";

export const PUT_PAYMENT_NOTE_STARTED = "PUT_PAYMENT_NOTE_STARTED";
export const PUT_PAYMENT_NOTE_SUCCESS = "PUT_PAYMENT_NOTE_SUCCESS";
export const PUT_PAYMENT_NOTE_FAILED = "PUT_PAYMENT_NOTE_FAILED";

export const DELETE_PAYMENT_NOTE_STARTED = "DELETE_PAYMENT_NOTE_STARTED";
export const DELETE_PAYMENT_NOTE_SUCCESS = "DELETE_PAYMENT_NOTE_SUCCESS";
export const DELETE_PAYMENT_NOTE_FAILED = "DELETE_PAYMENT_NOTE_FAILED";

export const getPaymentNotesStarted = () => ({
    type: GET_PAYMENT_NOTES_STARTED,
});
export const getPaymentNotesSuccess = (paymentNotes) => ({
    type: GET_PAYMENT_NOTES_SUCCESS,
    payload: paymentNotes,
});
export const getPaymentNotesFailed = (error) => ({
    type: GET_PAYMENT_NOTES_FAILED,
    payload: error,
});
export const setPaymentNotesTotal = (total) => ({
    type: SET_PAYMENT_NOTES_TOTAL,
    payload: total,
});

export const getMyPaymentNotesStarted = () => ({
    type: GET_MY_PAYMENT_NOTES_STARTED,
});
export const getMyPaymentNotesSuccess = (paymentNotes) => ({
    type: GET_MY_PAYMENT_NOTES_SUCCESS,
    payload: paymentNotes,
});
export const getMyPaymentNotesFailed = (error) => ({
    type: GET_MY_PAYMENT_NOTES_FAILED,
    payload: error,
});
export const setMyPaymentNotesTotal = (total) => ({
    type: SET_MY_PAYMENT_NOTES_TOTAL,
    payload: total,
});

export const createPaymentNoteStarted = () => ({
    type: POST_PAYMENT_NOTE_STARTED,
});
export const createPaymentNoteSuccess = (paymentNote) => ({
    type: POST_PAYMENT_NOTE_SUCCESS,
    payload: paymentNote,
});
export const createPaymentNoteFailed = (error) => ({
    type: POST_PAYMENT_NOTE_FAILED,
    payload: error,
});

export const updatePaymentNoteStarted = () => ({
    type: PUT_PAYMENT_NOTE_STARTED,
});
export const updatePaymentNoteSuccess = (paymentNote) => ({
    type: PUT_PAYMENT_NOTE_SUCCESS,
    payload: paymentNote,
});
export const updatePaymentNoteFailed = (error) => ({
    type: PUT_PAYMENT_NOTE_FAILED,
    payload: error,
});

export const deletePaymentNoteStarted = () => ({
    type: DELETE_PAYMENT_NOTE_STARTED,
});
export const deletePaymentNoteSuccess = (id) => ({
    type: DELETE_PAYMENT_NOTE_SUCCESS,
    payload: id,
});
export const deletePaymentNoteFailed = (error) => ({
    type: DELETE_PAYMENT_NOTE_FAILED,
    payload: error,
});