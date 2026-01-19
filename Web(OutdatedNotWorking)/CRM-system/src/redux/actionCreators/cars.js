export const GET_CARS_WITH_INFO_STARTED = "GET_CARS_WITH_INFO_STARTED";
export const GET_CARS_WITH_INFO_SUCCESS = "GET_CARS_WITH_INFO_SUCCESS";
export const GET_CARS_WITH_INFO_FAILED = "GET_CARS_WITH_INFO_FAILED";
export const SET_CARS_WITH_INFO_TOTAL = "SET_CARS_WITH_INFO_TOTAL";

export const GET_MY_CARS_STARTED = "GET_MY_CARS_STARTED";
export const GET_MY_CARS_SUCCESS = "GET_MY_CARS_SUCCESS";
export const GET_MY_CARS_FAILED = "GET_MY_CARS_FAILED";
export const SET_MY_CARS_TOTAL = "SET_MY_CARS_TOTAL";

export const GET_IN_WORK_CARS_STARTED = "GET_IN_WORK_CARS_STARTED";
export const GET_IN_WORK_CARS_SUCCESS = "GET_IN_WORK_CARS_SUCCESS";
export const GET_IN_WORK_CARS_FAILED = "GET_IN_WORK_CARS_FAILED";
export const SET_IN_WORK_CARS_TOTAL = "SET_IN_WORK_CARS_TOTAL";

export const POST_CAR_STARTED = "POST_CAR_STARTED";
export const POST_CAR_SUCCESS = "POST_CAR_SUCCESS";
export const POST_CAR_FAILED = "POST_CAR_FAILED";

export const PUT_CAR_STARTED = "PUT_CAR_STARTED";
export const PUT_CAR_SUCCESS = "PUT_CAR_SUCCESS";
export const PUT_CAR_FAILED = "PUT_CAR_FAILED";

export const DELETE_CAR_STARTED = "DELETE_CAR_STARTED";
export const DELETE_CAR_SUCCESS = "DELETE_CAR_SUCCESS";
export const DELETE_CAR_FAILED = "DELETE_CAR_FAILED";

export const getCarsWithInfoStarted = () => ({
    type: GET_CARS_WITH_INFO_STARTED,
});
export const getCarsWithInfoSuccess = (cars) => ({
    type: GET_CARS_WITH_INFO_SUCCESS,
    payload: cars,
});
export const getCarsWithInfoFailed = (error) => ({
    type: GET_CARS_WITH_INFO_FAILED,
    payload: error,
});
export const setCarsWithInfoTotal = (total) => ({
    type: SET_CARS_WITH_INFO_TOTAL,
    payload: total,
});

export const getMyCarsStarted = () => ({
    type: GET_MY_CARS_STARTED,
});
export const getMyCarsSuccess = (cars) => ({
    type: GET_MY_CARS_SUCCESS,
    payload: cars,
});
export const getMyCarsFailed = (error) => ({
    type: GET_MY_CARS_FAILED,
    payload: error,
});
export const setMyCarsTotal = (total) => ({
    type: SET_MY_CARS_TOTAL,
    payload: total,
});

export const getInWorkCarsStarted = () => ({
    type: GET_IN_WORK_CARS_STARTED,
});
export const getInWorkCarsSuccess = (cars) => ({
    type: GET_IN_WORK_CARS_SUCCESS,
    payload: cars,
});
export const getInWorkCarsFailed = (error) => ({
    type: GET_IN_WORK_CARS_FAILED,
    payload: error,
});
export const setInWorkCarsTotal = (total) => ({
    type: SET_IN_WORK_CARS_TOTAL,
    payload: total,
});

export const createCarStarted = () => ({
    type: POST_CAR_STARTED,
});
export const createCarSuccess = (car) => ({
    type: POST_CAR_SUCCESS,
    payload: car,
});
export const createCarFailed = (error) => ({
    type: POST_CAR_FAILED,
    payload: error,
});

export const updateCarStarted = () => ({
    type: PUT_CAR_STARTED,
});
export const updateCarSuccess = (car) => ({
    type: PUT_CAR_SUCCESS,
    payload: car,
});
export const updateCarFailed = (error) => ({
    type: PUT_CAR_FAILED,
    payload: error,
});

export const deleteCarStarted = () => ({
    type: DELETE_CAR_STARTED,
});
export const deleteCarSuccess = (id) => ({
    type: DELETE_CAR_SUCCESS,
    payload: id,
});
export const deleteCarFailed = (error) => ({
    type: DELETE_CAR_FAILED,
    payload: error,
});