import { GET_CATALOG_FAILED, GET_CATALOG_STARTED, GET_CATALOG_SUCCESS, SET_CATALOG_TOTAL } from "../actionCreators/catalogOfWorks";

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
            }

        default:
            return {
                ...state,
            };
    };
};