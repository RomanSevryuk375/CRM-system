import { api } from "../../api"
import { acceptWorkProposalFailed, acceptWorkProposalStarted, acceptWorkProposalSuccess, createWorkProposalFailed, createWorkProposalStarted, createWorkProposalSuccess, deleteWorkProposalFailed, deleteWorkProposalStarted, deleteWorkProposalSuccess, getWorkProposalInWorkFailed, getWorkProposalInWorkStarted, getWorkProposalInWorkSuccess, getWorkProposalWithInfoFailed, getWorkProposalWithInfoStarted, getWorkProposalWithInfoSuccess, rejectWorkProposalFailed, rejectWorkProposalStarted, rejectWorkProposalSuccess, setWorkProposalInWorkTotal, setWorkProposalWithInfoTotal, updateWorkProposalFailed, updateWorkProposalStarted, updateWorkProposalSuccess } from "../actionCreators/workProposals"

export const getWorkProposalWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getWorkProposalWithInfoStarted())

            const response = await api.workProposals.getworkProposalWithInfo({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setWorkProposalWithInfoTotal(totalCount));
            };

            dispatch(getWorkProposalWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getWorkProposalWithInfoFailed(error));
        }
    };
};

export const getWorkProposalInWork = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getWorkProposalInWorkStarted())

            const response = await api.workProposals.getWorkProposalInWork({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setWorkProposalInWorkTotal(totalCount));
            };

            dispatch(getWorkProposalInWorkSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getWorkProposalInWorkFailed(error));
        }
    };
};

export const createWorkProposal = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createWorkProposalStarted())

            const response = await api.workProposals.createWorkProposal(data);

            dispatch(createWorkProposalSuccess(response.data));
        } catch (error) {
            dispatch(createWorkProposalFailed(error));
        }
    };
};

export const updateWorkProposal = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateWorkProposalStarted());

            const response = await api.workProposals.updateWorkProposal(id, data);

            dispatch(updateWorkProposalSuccess(response.data));
        } catch (error) {
            dispatch(updateWorkProposalFailed(error));
        }
    };
};

export const acceptWorkProposal = (id) => {
    return async (dispatch) => {
        try {
            dispatch(acceptWorkProposalStarted())

            const response = await api.workProposals.acceptWorkProposal(id);

            dispatch(acceptWorkProposalSuccess(response.data));
        } catch (error) {
            dispatch(acceptWorkProposalFailed(error));
        }
    };
};

export const rejectWorkProposal = (id) => {
    return async (dispatch) => {
        try {
            dispatch(rejectWorkProposalStarted())

            const response = await api.workProposals.rejectWorkProposal(id);

            dispatch(rejectWorkProposalSuccess(response.data));
        } catch (error) {
            dispatch(rejectWorkProposalFailed(error));
        }
    };
};

export const deleteWorkProposal = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteWorkProposalStarted())

            const response = await api.workProposals.deleteWorkProposal(id);

            dispatch(deleteWorkProposalSuccess(response.data));
        } catch (error) {
            dispatch(deleteWorkProposalFailed(error));
        }
    };
};