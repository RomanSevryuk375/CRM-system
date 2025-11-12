export const GET_CATALOG_STARTED = "GET_CATALOG_STARTED";
export const GET_CATALOG_SUCCESS = "GET_CATALOG_SUCCESS";
export const GET_CATALOG_FAILED = "GET_CATALOG_FAILED";

export const getCatalogStarted = () => ({
    type: GET_CATALOG_STARTED,
});

export const getCatalogSuccess = (catalogOfWorks) => ({
    type: GET_CATALOG_SUCCESS,
    payload: catalogOfWorks,
});

export const getCatalogFailed = (error) => ({
    type: GET_CATALOG_FAILED,
    payload: error,
})