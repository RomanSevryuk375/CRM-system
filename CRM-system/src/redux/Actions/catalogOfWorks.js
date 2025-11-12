import { api } from "../../api";
import { getCatalogFailed, getCatalogSuccess, getCatalogStarted } from "../actionCreators/catalogOfWorks"

export const getCatalogOfWorks = () => {
    return async (dispatch) => {
        try {
            dispatch(getCatalogStarted());  
            const response = await api.catalogOfWorks.getCatalogOfWorks({
                // params: {
                //     _page: 0,
                //     _limit: 1,
                // },
            });
            dispatch(getCatalogSuccess(response.data));
        } catch (error) {
            dispatch(getCatalogFailed(error));
        }
    };
};