import { combineReducers } from "redux";
import { catalogReducer } from "./catalogOfWorks";
import { clientsReducer } from "./clients";
import { usersReducer } from "./users";

export const rootReducer = combineReducers({
    catalogOfWorks: catalogReducer,
    clients: clientsReducer,
    users: usersReducer,
});