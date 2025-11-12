import { GET_CATALOG_FAILED, GET_CATALOG_STARTED, GET_CATALOG_SUCCESS } from "../actionCreators/catalogOfWorks";

const initialeState = {
    catalogOfWorks: [],
    isCatalogOfWorksLoading: true,
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
                catalogOfWorks: action.payload,
                isCatalogOfWorksLoading: false,
            };

        case GET_CATALOG_FAILED:
            return {
                ...state,
                isCatalogOfWorksLoading: false,
            };

        default:
            return {
                ...state,
            };
    };
};