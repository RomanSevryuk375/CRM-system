import { api } from "../../api";
import { setMyPaymentNotesTotal } from "../actionCreators/paymentNotes";
import { getMyRepairNotesFailed, getMyRepairNotesStarted, getMyRepairNotesSuccess, getRepairNotesInWorkFailed, getRepairNotesInWorkStarted, getRepairNotesInWorkSuccess, getRepairNotesWithInfoFailed, getRepairNotesWithInfoStarted, getRepairNotesWithInfoSuccess, setRepairNotesInWorkTotal } from "../actionCreators/repairNotes"

export const getRepairNotesWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getRepairNotesWithInfoStarted());

            const response = await api.repairNotes.getRepairNotesWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setRepairNotesInWorkTotal(totalCount));
            }

            dispatch(getRepairNotesWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getRepairNotesWithInfoFailed(error));
        }
    };
};

export const getMyRepairNotes = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getMyRepairNotesStarted());

            const response = await api.repairNotes.getMyRepairNotes({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setMyPaymentNotesTotal(totalCount));
            };

            dispatch(getMyRepairNotesSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getMyRepairNotesFailed(error));
        }
    };
};

export const getRepairNotesInWork = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getRepairNotesInWorkStarted());

            const response = await api.repairNotes.getRepairNotesInWork({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setRepairNotesInWorkTotal(totalCount));
            };

            dispatch(getRepairNotesInWorkSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getRepairNotesInWorkFailed(error));
        }
    };
};