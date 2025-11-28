import { DELETE_CAR_FAILED, DELETE_CAR_STARTED, DELETE_CAR_SUCCESS, GET_CARS_WITH_INFO_FAILED, GET_CARS_WITH_INFO_STARTED, GET_CARS_WITH_INFO_SUCCESS, GET_IN_WORK_CARS_FAILED, GET_IN_WORK_CARS_STARTED, GET_IN_WORK_CARS_SUCCESS, GET_MY_CARS_FAILED, GET_MY_CARS_STARTED, GET_MY_CARS_SUCCESS, POST_CAR_FAILED, POST_CAR_STARTED, POST_CAR_SUCCESS, PUT_CAR_FAILED, PUT_CAR_STARTED, PUT_CAR_SUCCESS, SET_CARS_WITH_INFO_TOTAL, SET_IN_WORK_CARS_TOTAL, SET_MY_CARS_TOTAL } from "../actionCreators/cars";

const initialeState = {
    cars: [],
    myCars: [],
    inWorkCars: [],
    isCarsLoading: true,
    carsTotal: 0,
    myCarsTotal: 0,
    inWorkCarsTotal: 0,
};

export const carsReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_CARS_WITH_INFO_STARTED:
            return {
                ...state,
                isCarsLoading: true,
            };
        case GET_CARS_WITH_INFO_SUCCESS:
            return {
                ...state,
                cars: action.payload.page === 1
                    ? action.payload.data
                    : [...state.cars, ...action.payload.data],
                isCarsLoading: false,
            };
        case GET_CARS_WITH_INFO_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };
        case SET_CARS_WITH_INFO_TOTAL:
            return {
                ...state,
                carsTotal: action.payload,
            };

        case GET_MY_CARS_STARTED:
            return {
                ...state,
                isCarsLoading: true,
                myCars: action.page === 1 ? [] : state.myCars,
            };
        case GET_MY_CARS_SUCCESS:
            return {
                ...state,
                myCars: action.payload.page === 1
                    ? action.payload.data
                    : [...state.myCars, ...action.payload.data],
                isCarsLoading: false,
            };
        case GET_MY_CARS_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };
        case SET_MY_CARS_TOTAL:
            return {
                ...state,
                myCarsTotal: action.payload,
            };

        case GET_IN_WORK_CARS_STARTED:
            return {
                ...state,
                isCarsLoading: true,
            };
        case GET_IN_WORK_CARS_SUCCESS:
            return {
                ...state,
                inWorkCars: action.payload.page === 1
                    ? action.payload.data
                    : [...state.inWorkCars, ...action.payload.data],
                isCarsLoading: false,
            };
        case GET_IN_WORK_CARS_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };
        case SET_IN_WORK_CARS_TOTAL:
            return {
                ...state,
                inWorkCarsTotal: action.payload,
            };

        case POST_CAR_STARTED:
            return {
                ...state,
                isCarsLoading: true,
            };
        case POST_CAR_SUCCESS:
            return {
                ...state,
                cars: [action.payload, ...state.cars],
                isCarsLoading: false,
            };
        case POST_CAR_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };

        case PUT_CAR_STARTED:
            return {
                ...state,
                isCarsLoading: true,
            };
        case PUT_CAR_SUCCESS:
            return {
                ...state,
                cars: state.cars.map(item =>
                    item.id === action.payload.id ? action.payload : item),
            };
        case PUT_CAR_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };

        case DELETE_CAR_STARTED:
            return {
                ...state,
                isCarsLoading: true,
            };
        case DELETE_CAR_SUCCESS:
            return {
                ...state,
                cars: state.cars.filter(item => item.id !== action.payload),
                isCarsLoading: false,
            };
        case DELETE_CAR_FAILED:
            return {
                ...state,
                isCarsLoading: false,
            };

        default:
            return state;
    }
}