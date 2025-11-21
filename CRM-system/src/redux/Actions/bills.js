import { api } from "../../api"
import { createBillFailed, createBillStarted, createBillSuccess, getBillsFailed, getBillsStarted, getBillsSuccess, getMyBillsFailed, getMyBillsStarted, getMyBillsSuccess, setBillsTotal, setMyBillsTotal } from "../actionCreators/bills";

export const getBills = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getBillsStarted());

            const response = await api.bills.getBills({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setBillsTotal(totalCount));
            }

            dispatch(getBillsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getBillsFailed(error));
        }
    };
};

export const getMyBills = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getMyBillsStarted())

            const response = await api.bills.getMyBills({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setMyBillsTotal(totalCount));
            }

            dispatch(getMyBillsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getMyBillsFailed(error));
        }
    };
};

export const createBill = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createBillStarted());

            const response = await api.bills.createBill(data);

            dispatch(createBillSuccess(response.data));
        } catch (error) {
            dispatch(createBillFailed(error));
        }
    };
};