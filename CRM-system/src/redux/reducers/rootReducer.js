import { combineReducers } from "redux";
import { catalogReducer } from "./catalogOfWorks";
import { clientsReducer } from "./clients";

export const rootReducer = combineReducers({
    catalogOfWorks: catalogReducer,
    clients: clientsReducer,
});