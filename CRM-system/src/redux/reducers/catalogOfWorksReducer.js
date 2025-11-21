import { DELETE_WORK_TYPE_FAILED, DELETE_WORK_TYPE_STARTED, DELETE_WORK_TYPE_SUCCESS, GET_CATALOG_FAILED, GET_CATALOG_STARTED, GET_CATALOG_SUCCESS, POST_WORK_TYPE_FAILED, POST_WORK_TYPE_STARTED, POST_WORK_TYPE_SUCCESS, PUT_WORK_TYPE_FAILED, PUT_WORK_TYPE_STARTED, PUT_WORK_TYPE_SUCCESS, SET_CATALOG_TOTAL } from "../actionCreators/catalogOfWorks";

const initialeState = {
    catalogOfWorks: [],
    isCatalogOfWorksLoading: true,
    totalCatalog: 0,
};

export const catalogReducer = (state = initialeState, action) => {
    switch (action.type) {
        case GET_CATALOG_STARTED:
            return {
                ...state,
                isCatalogOfWorksLoading: true,
            };
        case GET_CATALOG_SUCCESS:
            return {
                ...state,
                catalogOfWorks:
                    action.payload.page === 1
                        ? action.payload.data
                        : [...state.catalogOfWorks, ...action.payload.data],
                isCatalogOfWorksLoading: false,
            };
        case GET_CATALOG_FAILED:
            return {
                ...state,
                isCatalogOfWorksLoading: false,
            };
        case SET_CATALOG_TOTAL:
            return {
                ...state,
                totalCatalog: action.payload,
            };

        case POST_WORK_TYPE_STARTED:
            return {
                ...state,
                isCatalogOfWorksLoading: true,
            };

        case POST_WORK_TYPE_SUCCESS:
            return {
                ...state,
                catalogOfWorks: [action.payload, ...state.catalogOfWorks],
                isCatalogOfWorksLoading: false,
            };
        case POST_WORK_TYPE_FAILED:
            return {
                ...state,
                isCatalogOfWorksLoading: false,
            };

        case PUT_WORK_TYPE_STARTED:
            return {
                ...state,
                isCatalogOfWorksLoading: true,
            };
        case PUT_WORK_TYPE_SUCCESS:
            return {
                ...state,
                catalogOfWorks: state.catalogOfWorks.map(item => item.id === action.payload.id ? action.payload : item),
                isCatalogOfWorksLoading: false,
            };
        case PUT_WORK_TYPE_FAILED:
            return {
                ...state,
                isCatalogOfWorksLoading: false,
            };

        case DELETE_WORK_TYPE_STARTED:
            return {
                ...state,
                isCatalogOfWorksLoading: true,
            };
        case DELETE_WORK_TYPE_SUCCESS:
            return {
                ...state,
                catalogOfWorks: state.catalogOfWorks.filter(item => item.id !== action.payload),
                isCatalogOfWorksLoading: false,
            };
        case DELETE_WORK_TYPE_FAILED:
            return {
                ...state,
                isCatalogOfWorksLoading: false,
            };

        default:
            return state;
    };
};