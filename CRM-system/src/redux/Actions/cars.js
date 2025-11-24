import { api } from "../../api";
import { createCarFailed, createCarStarted, createCarSuccess, deleteCarFailed, deleteCarStarted, deleteCarSuccess, getCarsWithInfoFailed, getCarsWithInfoStarted, getCarsWithInfoSuccess, getInWorkCarsFailed, getInWorkCarsStarted, getInWorkCarsSuccess, getMyCarsFailed, getMyCarsStarted, getMyCarsSuccess, setCarsWithInfoTotal, setInWorkCarsTotal, setMyCarsTotal, updateCarFailed, updateCarStarted, updateCarSuccess } from "../actionCreators/cars"

export const getCarsWithInfo = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getCarsWithInfoStarted());

            const response = await api.cars.getCarsWithInfo({
                params: {
                    _page: page,
                    _limit: 25, 
                },
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setCarsWithInfoTotal(totalCount));
            }

            dispatch(getCarsWithInfoSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getCarsWithInfoFailed(error));
        }
    };
};

export const getMyCars = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getMyCarsStarted(page));

            const response = await api.cars.getMyCars({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setMyCarsTotal(totalCount));
            };

            dispatch(getMyCarsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getMyCarsFailed(error));
        }
    };
};

export const getInWorkCars = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getInWorkCarsStarted())

            const response = await api.cars.getInWorkCars({
                params: {
                    _page: page,
                    _limit: 25,
                }
            });

            const totalCount = parseInt(response.headers["x-total-count"], 10);
            if (!isNaN(totalCount)) {
                dispatch(setInWorkCarsTotal(totalCount));
            };

            dispatch(getInWorkCarsSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getInWorkCarsFailed(error));
        }
    };
};

export const createCar = (data) => {
    return async (dispatch) => {
        try {
            dispatch(createCarStarted());

            const response = await api.cars.createCar(data);

            dispatch(createCarSuccess(response.data));
            return response.data
        } catch (error) {
            dispatch(createCarFailed(error));
            return error
        }
    };
};

export const updateCar = (id, data) => {
    return async (dispatch) => {
        try {
            dispatch(updateCarStarted());

            const response = await api.cars.updateCar(id, data);
            
            dispatch(updateCarSuccess(response.data));
        } catch (error) {
            dispatch(updateCarFailed(error));
        }
    };
};

export const deleteCar = (id) => {
    return async (dispatch) => {
        try {
            dispatch(deleteCarStarted());

            const response = await api.cars.deleteCar(id);

            dispatch(deleteCarSuccess(response.data));
        } catch (error) {
            dispatch(deleteCarFailed(error));
        }
    };
};