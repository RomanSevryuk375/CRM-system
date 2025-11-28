export const GET_WORK_PROPOSAL_WITH_INFO_STARTED = "GET_WORK_PROPOSAL_WITH_INFO_STARTED";
export const GET_WORK_PROPOSAL_WITH_INFO_SUCCESS = "GET_WORK_PROPOSAL_WITH_INFO_SUCCESS";
export const GET_WORK_PROPOSAL_WITH_INFO_FAILED = "GET_WORK_PROPOSAL_WITH_INFO_FAILED";
export const SET_WORK_PROPOSAL_WITH_INFO_TOTAL = "SET_WORK_PROPOSAL_WITH_INFO_TOTAL";

export const GET_WORK_PROPOSAL_IN_WORK_STARTED = "GET_WORK_PROPOSAL_IN_WORK_STARTED";
export const GET_WORK_PROPOSAL_IN_WORK_SUCCESS = "GET_WORK_PROPOSAL_IN_WORK_SUCCESS";
export const GET_WORK_PROPOSAL_IN_WORK_FAILED = "GET_WORK_PROPOSAL_IN_WORK_FAILED";
export const SET_WORK_PROPOSAL_IN_WORK_TOTAL = "SET_WORK_PROPOSAL_IN_WORK_TOTAL";

export const POST_WORK_PROPOSAL_STARTED = "POST_WORK_PROPOSAL_STARTED";
export const POST_WORK_PROPOSAL_SUCCESS = "POST_WORK_PROPOSAL_SUCCESS";
export const POST_WORK_PROPOSAL_FAILED = "POST_WORK_PROPOSAL_FAILED";

export const PUT_WORK_PROPOSAL_STARTED = "PUT_WORK_PROPOSAL_STARTED";
export const PUT_WORK_PROPOSAL_SUCCESS = "PUT_WORK_PROPOSAL_SUCCESS";
export const PUT_WORK_PROPOSAL_FAILED = "PUT_WORK_PROPOSAL_FAILED";

export const PUT_WORK_PROPOSAL_ACCEPT_STARTED = "PUT_WORK_PROPOSAL_ACCEPT_STARTED";
export const PUT_WORK_PROPOSAL_ACCEPT_SUCCESS = "PUT_WORK_PROPOSAL_ACCEPT_SUCCESS";
export const PUT_WORK_PROPOSAL_ACCEPT_FAILED = "PUT_WORK_PROPOSAL_ACCEPT_FAILED";

export const PUT_WORK_PROPOSAL_REJECT_STARTED = "PUT_WORK_PROPOSAL_REJECT_STARTED";
export const PUT_WORK_PROPOSAL_REJECT_SUCCESS = "PUT_WORK_PROPOSAL_REJECT_SUCCESS";
export const PUT_WORK_PROPOSAL_REJECT_FAILED = "PUT_WORK_PROPOSAL_REJECT_FAILED";

export const DELETE_WORK_PROPOSAL_STARTED = "DELETE_WORK_PROPOSAL_STARTED";
export const DELETE_WORK_PROPOSAL_SUCCESS = "DELETE_WORK_PROPOSAL_SUCCESS";
export const DELETE_WORK_PROPOSAL_FAILED = "DELETE_WORK_PROPOSAL_FAILED";

export const getWorkProposalWithInfoStarted = () => ({
    type: GET_WORK_PROPOSAL_WITH_INFO_STARTED,
});
export const getWorkProposalWithInfoSuccess = (workProposals) => ({
    type: GET_WORK_PROPOSAL_WITH_INFO_SUCCESS,
    payload: workProposals,
});
export const getWorkProposalWithInfoFailed = (error) => ({
    type: GET_WORK_PROPOSAL_WITH_INFO_FAILED,
    payload: error,
});
export const setWorkProposalWithInfoTotal = (total) => ({
    type: SET_WORK_PROPOSAL_WITH_INFO_TOTAL,
    payload: total,
});

export const getWorkProposalInWorkStarted = () => ({
    type: GET_WORK_PROPOSAL_IN_WORK_STARTED,
});
export const getWorkProposalInWorkSuccess = (workProposals) => ({
    type: GET_WORK_PROPOSAL_IN_WORK_SUCCESS,
    payload: workProposals,
});
export const getWorkProposalInWorkFailed = (error) => ({
    type: GET_WORK_PROPOSAL_IN_WORK_FAILED,
    payload: error,
});
export const setWorkProposalInWorkTotal = (total) => ({
    type: SET_WORK_PROPOSAL_IN_WORK_TOTAL,
    payload: total,
});

export const createWorkProposalStarted = () => ({
    type: POST_WORK_PROPOSAL_STARTED,
});
export const createWorkProposalSuccess = (workProposal) => ({
    type: POST_WORK_PROPOSAL_SUCCESS,
    payload: workProposal,
});
export const createWorkProposalFailed = (error) => ({
    type: POST_WORK_PROPOSAL_FAILED,
    payload: error,
});

export const updateWorkProposalStarted = () => ({
    type: PUT_WORK_PROPOSAL_STARTED,
});
export const updateWorkProposalSuccess = (workProposal) => ({
    type: PUT_WORK_PROPOSAL_SUCCESS,
    payload: workProposal,
});
export const updateWorkProposalFailed = (error) => ({
    type: PUT_WORK_PROPOSAL_FAILED,
    payload: error,
});

export const acceptWorkProposalStarted = () => ({
    type: PUT_WORK_PROPOSAL_ACCEPT_STARTED,
});
export const acceptWorkProposalSuccess = (workProposal) => ({
    type: PUT_WORK_PROPOSAL_ACCEPT_SUCCESS,
    payload: workProposal,
});
export const acceptWorkProposalFailed = (error) => ({
    type: PUT_WORK_PROPOSAL_ACCEPT_FAILED,
    payload: error,
});

export const rejectWorkProposalStarted = () => ({
    type: PUT_WORK_PROPOSAL_REJECT_STARTED,
});
export const rejectWorkProposalSuccess = (workProposal) => ({
    type: PUT_WORK_PROPOSAL_REJECT_SUCCESS,
    payload: workProposal,
});
export const rejectWorkProposalFailed = (error) => ({
    type: PUT_WORK_PROPOSAL_REJECT_FAILED,
    payload: error,
});

export const deleteWorkProposalStarted = () => ({
    type: DELETE_WORK_PROPOSAL_STARTED,
});
export const deleteWorkProposalSuccess = (id) => ({
    type: DELETE_WORK_PROPOSAL_SUCCESS,
    payload: id,
});
export const deleteWorkProposalFailed = (error) => ({
    type: DELETE_WORK_PROPOSAL_FAILED,
    payload: error,
});