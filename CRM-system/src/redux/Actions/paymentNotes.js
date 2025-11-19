import { api } from "../../api"
import { createPaymentNoteFailed, createPaymentNoteStarted, createPaymentNoteSuccess, deletePaymentNoteFailed, deletePaymentNoteStarted, deletePaymentNoteSuccess, getMyPaymentNotesFailed, getMyPaymentNotesStarted, getPaymentNotesFailed, getPaymentNotesStarted, getPaymentNotesSuccess, setMyPaymentNotesTotal, setPaymentNotesTotal, updatePaymentNoteFailed, updatePaymentNoteStarted, updatePaymentNoteSuccess } from "../actionCreators/paymentNotes"

export const getPaymentNotes = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getPaymentNotesStarted())

            const response = await api.paymentNotes.getPaymentNotes({
                params: { 
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setPaymentNotesTotal(totalCount));
            }

            dispatch(getPaymentNotesSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getPaymentNotesFailed(error));
        }
    };
};

export const getMyPaymentNotes = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getMyPaymentNotesStarted())

            const response = await api.paymentNotes.getMyPaymentNotes({
                params:{
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setMyPaymentNotesTotal(totalCount));
            };

            dispatch(getMyPaymentNotesStarted({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getMyPaymentNotesFailed(error));
        }
    };
};

export const createPaymentNote = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createPaymentNoteStarted());

            const response = await api.paymentNotes.createPaymentNote(data);

            dispatch(createPaymentNoteSuccess(response.data));
        } catch (error) {
            dispatch(createPaymentNoteFailed(error));
        }
    };
};

export const updatePaymentNote = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updatePaymentNoteStarted());

            const response = await api.paymentNotes.updatePaymentNote(id, data);

            dispatch(updatePaymentNoteSuccess(response.data));
        } catch (error) {
            dispatch(updatePaymentNoteFailed(error));
        }
    };
};

export const deletePaymentNote = (id) => {
    return async (dispatch) => { 
        try {
            dispatch(deletePaymentNoteStarted());

            const response = await api.paymentNotes.deletePaymentNote(id);

            dispatch(deletePaymentNoteSuccess(response.data));
        } catch (error) {
            dispatch(deletePaymentNoteFailed(error));
        }
    };
};