import { api } from "../../api";
import { getCatalogFailed, getCatalogSuccess, getCatalogStarted, setCatalogTotal } from "../actionCreators/catalogOfWorks"

export const getCatalogOfWorks = (page = 1) => {
    return async (dispatch) => {
        try {
            dispatch(getCatalogStarted());

            const response = await api.catalogOfWorks.getCatalogOfWorks({
                params: {
                    _page: page,
                    _limit: 25,
                },
            });
            
            const totalCount = parseInt(response.headers['x-total-count'], 10);
            if (!isNaN(totalCount)) {
                dispatch(setCatalogTotal(totalCount));
            }

            dispatch(getCatalogSuccess({
                data: response.data,
                page,
            }));
        } catch (error) {
            dispatch(getCatalogFailed(error));
        }
    };
};